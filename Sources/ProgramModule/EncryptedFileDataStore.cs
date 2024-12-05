using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Windows.Input;

public class EncryptedDataStore
{
    private readonly string folder;
    private readonly byte[] encryptionKey = Encoding.UTF8.GetBytes(GetEncryptKey());

    private static string GetEncryptKey()
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("AutoSplitterCore.appsettings.json"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonContent = reader.ReadToEnd();
                var jsonObject = JObject.Parse(jsonContent);
                String key = jsonObject["Encrypt"]["EncryptKey"].ToString();

                if (key.Length > 32) { key = key.Substring(0, 32); }
                else if (key.Length < 32)
                {
                    key = key.PadRight(32, '0'); // Rellenar con '0' si la longitud es menor de 32 } return key;
                }
                return key;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error on Incrusted Resource: ", ex);
        }
    }

    public EncryptedDataStore(string folder)
    {
        this.folder = folder;
    }

    public void Store<T>(string key, T value)
    {
        string jsonData = JsonConvert.SerializeObject(value);
        string encryptedData = Encrypt(jsonData);
        string filePath = GetFilePath(key);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        File.WriteAllText(filePath, encryptedData);
    }

    public T Get<T>(string key)
    {
        string filePath = GetFilePath(key);
        if (!File.Exists(filePath)) return default(T);

        string encryptedData = File.ReadAllText(filePath);
        string jsonData = Decrypt(encryptedData);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    public void Clear()
    {
        if (Directory.Exists(folder))
        {
            Directory.Delete(folder, true);
        }
    }

    private string Encrypt(string plainText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = encryptionKey;
            aes.GenerateIV();
            var iv = aes.IV;

            using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
            using (var ms = new MemoryStream())
            {
                ms.Write(iv, 0, iv.Length);
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cs))
                {
                    writer.Write(plainText);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private string Decrypt(string cipherText)
    {
        var fullCipher = Convert.FromBase64String(cipherText);
        using (var aes = Aes.Create())
        {
            aes.Key = encryptionKey;
            var iv = new byte[aes.BlockSize / 8];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Array.Copy(fullCipher, iv, iv.Length);
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
            using (var ms = new MemoryStream(cipher))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var reader = new StreamReader(cs))
            {
                return reader.ReadToEnd();
            }
        }
    }

    private string GetFilePath(string key)
    {
        return Path.Combine(folder, key);
    }
}

public class FileDataStoreAdapter : IDataStore
{
    private readonly EncryptedDataStore dataStore;

    public FileDataStoreAdapter(EncryptedDataStore dataStore)
    {
        this.dataStore = dataStore;
    }

    public Task ClearAsync()
    {
        dataStore.Clear();
        return Task.CompletedTask;
    }

    public Task DeleteAsync<T>(string key)
    {
        dataStore.Clear(); // Puedes ajustar esto para eliminar una clave específica
        return Task.CompletedTask;
    }

    public Task<T> GetAsync<T>(string key)
    {
        return Task.FromResult(dataStore.Get<T>(key));
    }

    public Task StoreAsync<T>(string key, T value)
    {
        dataStore.Store(key, value);
        return Task.CompletedTask;
    }
}
