const dllCategories = [
  {
    title: "APIS - Google API Libraries",
    items: [
      {
        name: "Google.Api.CommonProtos",
        author: "Google LLC",
        source: "https://github.com/googleapis/gax-dotnet",
      },
      {
        name: "Google.Api.Gax",
        author: "Google LLC",
        source: "https://github.com/googleapis/gax-dotnet",
      },
      {
        name: "Google.Api.Gax.Grpc",
        author: "Google LLC",
        source: "https://github.com/googleapis/gax-dotnet",
      },
      {
        name: "Google.Api.Gax.Rest",
        author: "Google LLC",
        source: "https://github.com/googleapis/gax-dotnet",
      },
      {
        name: "Google.Apis",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-api-dotnet-client",
      },
      {
        name: "Google.Apis.Auth",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-api-dotnet-client",
      },
      {
        name: "Google.Apis.Core",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-api-dotnet-client",
      },
      {
        name: "Google.Apis.Drive.v3",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-api-dotnet-client",
      },
      {
        name: "Google.Apis.Oauth2.v2",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-api-dotnet-client",
      },
      {
        name: "Google.Cloud.Firestore",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-cloud-dotnet",
      },
      {
        name: "Google.Cloud.Firestore.V1",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-cloud-dotnet",
      },
      {
        name: "Google.Cloud.Location",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-cloud-dotnet",
      },
      {
        name: "Google.LongRunning",
        author: "Google LLC",
        source: "https://github.com/googleapis/google-cloud-dotnet",
      },
      {
        name: "Google.Protobuf",
        author: "Google LLC",
        source: "https://github.com/protocolbuffers/protobuf",
      },
    ],
    license: "Apache License 2.0",
  },
  {
    title: "GRPC Libraries",
    items: [
      {
        name: "Grpc.Auth",
        author: "The gRPC Authors",
        source: "https://github.com/grpc/grpc",
      },
      {
        name: "Grpc.Core",
        author: "The gRPC Authors",
        source: "https://github.com/grpc/grpc",
      },
      {
        name: "Grpc.Core.Api",
        author: "The gRPC Authors",
        source: "https://github.com/grpc/grpc",
      },
      {
        name: "Grpc.Net.Client",
        author: "The gRPC Authors",
        source: "https://github.com/grpc/grpc-dotnet",
      },
      {
        name: "Grpc.Net.Common",
        author: "The gRPC Authors",
        source: "https://github.com/grpc/grpc-dotnet",
      },
    ],
    license: "Apache License 2.0",
  },
  {
    title: "Other Libraries",
    items: [
      {
        name: "Irony",
        author: "Roman Ivantsov",
        source: "https://github.com/IronyProject/Irony",
      },
      {
        name: "Microsoft.Bcl.AsyncInterfaces",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "Microsoft.Extensions.DependencyInjection.Abstractions",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/extensions",
      },
      {
        name: "Microsoft.Extensions.Logging.Abstractions",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/extensions",
      },
      {
        name: "Newtonsoft.Json",
        author: "James Newton-King",
        source: "https://github.com/JamesNK/Newtonsoft.Json",
      },
    ],
    license: "MIT",
  },
  {
    title: "System Libraries",
    items: [
      {
        name: "System.Buffers",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.CodeDom",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.Collections.Immutable",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.Diagnostics.DiagnosticSource",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.Linq.Async",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/reactive",
      },
      {
        name: "System.Management",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.Memory",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.Net.Http.WinHttpHandler",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.Numerics.Vectors",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.Runtime.CompilerServices.Unsafe",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.Threading.Tasks.Extensions",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
      {
        name: "System.ValueTuple",
        author: "Microsoft Corporation",
        source: "https://github.com/dotnet/runtime",
      },
    ],
    license: "MIT",
  },
  {
    title: "WEB",
    items: [
      {
        name: "React",
        author: "Meta",
        source: "https://github.com/facebook/react",
      },
      {
        name: "TailwindCSS",
        author: "Adam Wathan",
        source: "https://github.com/tailwindlabs/tailwindcss",
      },
    ],
    license: "MIT",
  },
];

export const ApiInfo = () => {
  return (
    <>
      <div className="max-w-6xl mx-left space-y-8 p-6 border rounded bg-yellow-500 text-center m-3 text-left">
        <section id="dll-used" className="bg-white p-6 rounded-lg shadow">
          <h2 className="text-2xl font-bold text-gray-800 mb-4">DLLs Used</h2>
          {dllCategories.map((category, index) => (
            <div key={index} className="mb-6">
              <h3 className="text-xl font-semibold text-gray-700 mb-2">
                {category.title}
              </h3>
              <ul className="list-disc pl-5 space-y-2">
                {category.items.map((item, itemIndex) => (
                  <li key={itemIndex}>
                    {item.name}
                    <br />
                    <strong>Author:</strong> {item.author}
                    <br />
                    <strong>Source:</strong>{" "}
                    <a href={item.source} target="_blank">
                      GitHub
                    </a>
                  </li>
                ))}
              </ul>
              <p className="mt-2">
                <strong>License for {category.title}:</strong>{" "}
                {category.license}
              </p>
            </div>
          ))}
        </section>
      </div>
    </>
  );
};
