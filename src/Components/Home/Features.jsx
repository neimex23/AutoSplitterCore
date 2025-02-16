export const Features = () => {
  return (
    <>
      <div className="max-w-3xl mx-auto space-y-8 p-6">
        <div
          className="bg-gray-800 text-white p-6 rounded-lg shadow-lg"
          id="features"
        >
          <div className="max-w-3xl mx-auto space-y-8 p-6">
            <h2 className="text-blue-500 text-2xl font-bold">Features</h2>
            <ul className="list-disc pl-6 mt-2">
              <li>
                AutoSplit for various games including Dark Souls, Sekiro, Elden
                Ring, Hollow Knight, and more.
              </li>
              <li>Auto-start timer when the game run starts.</li>
              <li>
                Support for In-Game Time or Real-Time values in OBS layouts.
              </li>
              <li>Save settings with a compatible XML file.</li>
              <li>
                Upload and Download Profile Configurations in Cloud Profile.
              </li>
              <li>Links Profiles HCM with ASC.</li>
            </ul>
          </div>
        </div>

        {/* AutoMods Features */}
        <div
          className="bg-gray-800 text-white p-6 rounded-lg shadow-lg"
          id="automods-features"
        >
          <h2 className="text-green-500 text-2xl font-bold">AutoMods</h2>
          <ul className="list-disc pl-6 mt-2">
            <li>Sekiro: No Logos Intro, No Tutorials.</li>
            <li>Elden Ring: No Logos Intro.</li>
          </ul>
        </div>

        {/* Installation */}
        <div
          className="bg-gray-800 text-white p-6 rounded-lg shadow-lg"
          id="installation"
        >
          <h2 className="text-violet-500 text-2xl font-bold">Installation</h2>
          <ul className="list-disc pl-6 mt-2">
            <li>
              <strong>Portable:</strong> Decompress the ZIP file and copy its
              content to the installation directory of HitCounterManager.
            </li>
            <li>
              <strong>Installer:</strong> Install the MSI file in the same
              folder as HitCounterManager. The installer will handle previous
              versions automatically.
            </li>
          </ul>
          <blockquote className="mt-4 border-l-4 border-yellow-500 pl-4 italic text-yellow-300">
            Ensure MSI is installed before updating to a new version.
          </blockquote>
        </div>

        {/* Issues or Bugs */}
        <div
          className="bg-gray-800 text-white p-6 rounded-lg shadow-lg"
          id="issues-bugs"
        >
          <h2 className="text-2xl font-bold">Issues or Bugs</h2>
          <ul className="list-disc pl-6 mt-2">
            <li>
              Report issues via{" "}
              <a
                href="https://github.com/neimex23/AutoSplitterCore/issues"
                className="text-blue-400 hover:underline"
              >
                GitHub Issues
              </a>
              .
            </li>
            <li>
              Contact via email:{" "}
              <a
                href="mailto:ezequielmedina23@gmail.com"
                className="text-blue-400 hover:underline"
              >
                ezequielmedina23@gmail.com
              </a>
              .
            </li>
            <li>
              Twitch:{" "}
              <a
                href="https://www.twitch.tv/neimex23"
                className="text-blue-400 hover:underline"
              >
                Neimex23
              </a>
              .
            </li>
            <li>
              Discord: <strong className="text-gray-300">Neimex23#6674</strong>.
            </li>
          </ul>
        </div>
      </div>
    </>
  );
};
