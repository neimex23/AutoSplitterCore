const customItems = [
  {
    name: "DLL SoulsMemory (SoulSplitter)",
    author: "F.stam (FrankvdStam)",
    source: "https://github.com/FrankvdStam/SoulSplitter.git",
    license: "GNU",
    attributions: [
      {
        name: "IGT Save File Dark Souls 1",
        author: "CapitaineToinon",
        source: "https://github.com/CapitaineToinon/LiveSplit.DarkSoulsIGT",
        license: "GNU",
      },
      {
        name: "DarkSouls3RemoveIntroScreens",
        author: "Blade",
        source: "https://github.com/bladecoding/DarkSouls3RemoveIntroScreens",
        license: "MIT",
      },
    ],
  },
  {
    name: "DLL LiveSplit.HollowKnight",
    author: "DevilSquirrel",
    source: "https://github.com/ShootMe/LiveSplit.HollowKnight.git",
    license: "MIT",
  },
  {
    name: "DLL LiveSplit.Celeste",
    author: "DevilSquirrel",
    source: "https://github.com/ShootMe/LiveSplit.Celeste.git",
    license: "MIT",
  },
  {
    name: "DLL LiveSplit.Cuphead",
    author: "DevilSquirrel",
    source: "https://github.com/ShootMe/LiveSplit.Cuphead.git",
    license: "MIT",
  },
  {
    name: "DLL LiveSplit.Dishonored (Modified)",
    author: "Fatalis",
    source: "https://github.com/fatalis/LiveSplit.Dishonored.git",
    license: "MIT (WTFPL)",
  },
  {
    name: "DLL LiveSplit.Core",
    author: "LiveSplit",
    source: "https://github.com/LiveSplit/LiveSplit",
    license: "MIT",
    additionalNotes: "Includes part of the code from LiveSplit.View.",
  },
  {
    name: "DLL ReaLTaiizor",
    author: "Taiizor",
    source: "https://github.com/Taiizor/ReaLTaiizor",
    license: "MIT",
  },
  {
    name: "Addon Developed for HitCounterManager",
    author: "Peter Kirmeier",
    contact: "topeterk@freenet.de",
    source: "https://github.com/topeterk/HitCounterManager",
    license: "MIT",
  },
];

const resourcesItems = [
  {
    name: "Setup AutoSplitterCore Banner Image",
    author: "Neimex23",
    license: "MIT",
    additionalNotes: "Using: Setup AutoSplitterCore Icon",
  },
  {
    name: "Setup AutoSplitterCore Icon",
    file: "AutoSplitterSetupIcon.ico",
    author: "Elionas",
    source: "https://pixabay.com/images/id-1345869/",
    license: "Pixabay License",
  },
  {
    name: "Check Icon",
    title: "icono-símbolo-confirmación-gancho-803718",
    author: "Leovinus",
    source: "https://pixabay.com/images/id-803718/",
    license: "Pixabay License",
  },
];

export const CustomDll = () => {
  return (
    <>
      <div className="max-w-5xl ml-auto space-y-8 p-6 border rounded bg-red-200 text-left">
        <section id="customs" className="bg-white p-6 rounded-lg shadow">
          <h2 className="text-2xl font-bold text-gray-800 mb-4">Customs</h2>
          {customItems.map((item, index) => (
            <article
              key={index}
              className="mb-6 border-b border-gray-200 pb-4 last:border-b-0">
              <h3 className="text-xl font-semibold text-gray-700">
                {item.name}
              </h3>
              <ul className="mt-2 space-y-1">
                <li>
                  <strong>Author:</strong> {item.author}
                </li>
                <li>
                  <strong>Source:</strong>{" "}
                  <a
                    href={item.source}
                    target="_blank"
                    rel="noopener noreferrer">
                    GitHub
                  </a>
                </li>
                <li>
                  <strong>License:</strong> {item.license}
                </li>
                {item.attributions && (
                  <li>
                    <strong>Attributions:</strong>
                    <ul className="list-disc pl-5 mt-1">
                      {item.attributions.map((attr, attrIndex) => (
                        <li key={attrIndex}>
                          {attr.name} - {attr.author} (
                          <a
                            href={attr.source}
                            target="_blank"
                            rel="noopener noreferrer">
                            GitHub
                          </a>
                          ) - License: {attr.license}
                        </li>
                      ))}
                    </ul>
                  </li>
                )}
                {item.additionalNotes && <li>{item.additionalNotes}</li>}
                {item.contact && (
                  <li>
                    <strong>Contact:</strong>{" "}
                    <a href={`mailto:${item.contact}`}>{item.contact}</a>
                  </li>
                )}
                {item.file && (
                  <li>
                    <strong>File:</strong> {item.file}
                  </li>
                )}
                {item.title && (
                  <li>
                    <strong>Title:</strong> {item.title}
                  </li>
                )}
              </ul>
            </article>
          ))}
        </section>
        <div className="border bg-white rounded p-1">
          <section id="resources" className="section">
            <h2 className="text-2xl font-bold text-gray-800 mb-4">Resources</h2>
            {resourcesItems.map((item, index) => (
              <article
                key={index}
                className="mb-6 border-b border-gray-200 pb-4 last:border-b-0">
                <h3 className="text-xl font-semibold text-gray-700">
                  {item.name}
                </h3>
                <ul className="mt-2 space-y-1">
                  {item.author && (
                    <li>
                      <strong>Author:</strong> {item.author}
                    </li>
                  )}
                  {item.source && (
                    <li>
                      <strong>Source:</strong>{" "}
                      <a
                        href={item.source}
                        target="_blank"
                        rel="noopener noreferrer">
                        {item.source.includes("pixabay") ? "Pixabay" : "GitHub"}
                      </a>
                    </li>
                  )}
                  {item.license && (
                    <li>
                      <strong>License:</strong> {item.license}
                    </li>
                  )}
                  {item.additionalNotes && <li>{item.additionalNotes}</li>}
                  {item.file && (
                    <li>
                      <strong>File:</strong> {item.file}
                    </li>
                  )}
                  {item.title && (
                    <li>
                      <strong>Title:</strong> {item.title}
                    </li>
                  )}
                </ul>
              </article>
            ))}
          </section>
        </div>
      </div>
    </>
  );
};
