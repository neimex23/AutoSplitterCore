import React from "react";

const info = [
  {
    name: "AutoSplitterCore",
    author: "Ezequiel Medina (Neimex23)",
    contact: "neimex23@gmail.com",
    license: "MIT",
  },
  {
    name: "IGT Timer Track",
    author: "Hauke Lasinger",
    contact: "hauke.lasinger@gmail.com",
    license: "MIT",
  },
  {
    name: "IGT Timer Track Test",
    author: "Mario Schulz",
    contact: "mario.schulz@gmail.com",
    license: "MIT",
  },
];

export const ContactInfo = () => {
  return (
    <div className="max-w-3xl mx-auto space-y-8 p-6 border rounded bg-blue-200">
      <section id="components" className="bg-white p-6 rounded-lg shadow">
        <h2 className="text-2xl font-bold text-gray-800 mb-4">Components</h2>
        <div className="space-y-6">
          {info.map((info, index) => (
            <article
              key={index}
              className="border-b border-gray-200 pb-4 last:border-b-0">
              <h3 className="text-xl font-semibold text-gray-700">
                {info.name}
              </h3>
              <ul className="mt-2 space-y-1">
                <li>
                  <strong>Author:</strong> {info.author}
                </li>
                <li>
                  <strong>Contact:</strong>{" "}
                  <a
                    href={`mailto:${info.contact}`}
                    className="text-blue-600 hover:underline">
                    {info.contact}
                  </a>
                </li>
                <li>
                  <strong>License:</strong> {info.license}
                </li>
              </ul>
            </article>
          ))}
        </div>
      </section>
    </div>
  );
};
