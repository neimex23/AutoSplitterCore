import React, { useState } from "react";

const games = [
  {
    title: "Sekiro",
    description: "Immediately/Loading After",
    flags: [
      "Kill a Boss",
      "Is Activated a Idol",
      "Trigger a position",
      "Custom Flag",
    ],
    image:
      "https://media.fromsoftware.jp/fromsoftware/jp/static/img/products/detail/screenshot/product_104_1.jpg",
  },
  {
    title: "Elden Ring",
    description: "Immediately/Loading After",
    flags: ["Kill a Boss", "Is Activated a Grace", "Trigger a position"],
    image: "https://media.techtribune.net/uploads/2023/04/sds5.jpg",
  },
  {
    title: "Dark Souls 1",
    description: "Immediately/Loading Game After",
    flags: [
      "Kill a Boss",
      "Active a Bonfire",
      "Level the Character",
      "Trigger a Position",
      "Obtain an Item",
    ],
    image:
      "https://www.dreadxp.com/wp-content/uploads/2022/09/ss_c34cdf130b9ac71c99196007d1e78c05305652b9.1920x1080.jpg",
  },
  {
    title: "Dark Souls 2",
    description: "Immediately/Loading After",
    flags: ["Kill a Boss", "Level the Character", "Trigger a position"],
    image:
      "https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/236430/capsule_616x353.jpg?t=1726158600",
  },
  {
    title: "Dark Souls 3",
    description: "Immediately/Loading After",
    flags: ["Kill a Boss", "Is Activated a Bonfire", "Level the Character"],
    image:
      "https://easycdn.es/1/v/video_34812.jpg",
  },
  {
    title: "Hollow Knight",
    description: "Immediately",
    flags: [
      "Kill a Boss/Minibosses",
      "Dreamers/Kills Events",
      "Colosseum",
      "Pantheons",
      "Charms/Skills",
      "Trigger a position",
    ],
    image:
      "https://images.squarespace-cdn.com/content/v1/606d159a953867291018f801/1619987722169-VV6ZASHHZNRBJW9X0PLK/Key_Art_02_layeredjpg.jpg?format=1500w",
  },
  {
    title: "Celeste",
    description: "Immediately",
    flags: ["Chapter", "Checkpoints"],
    image:
      "https://i.blogs.es/442f82/020119-celeste/1366_2000.jpg",
  },
  {
    title: "Cuphead",
    description: "Immediately",
    flags: ["Kill a Boss", "Complete a Level"],
    image:
      "https://media.comunidadxbox.com/wp-content/uploads/2019/09/Cuphead-Comunidad-Xbox-1.png",
  },
  {
    title: "Dishonored",
    description: "Immediately",
    flags: [
      "Intro End",
      "Mission End",
      "Prison Escape (Sewer Entrance)",
      "Outsider's Dream",
      "Weepers",
    ],
    image:
      "https://cdn.hobbyconsolas.com/sites/navi.axelspringer.es/public/media/image/2020/07/dishonored-2-1981503.jpg",
  },
];

export const AutoSplitterFlags = () => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const [animation, setAnimation] = useState("");

  const changeCard = (direction) => {
    setAnimation(
      direction === "next"
        ? "translate-x-10 opacity-0"
        : "-translate-x-10 opacity-0"
    );
    setTimeout(() => {
      setCurrentIndex((prevIndex) =>
        direction === "next"
          ? (prevIndex + 1) % games.length
          : (prevIndex - 1 + games.length) % games.length
      );
      setAnimation("opacity-0");
      setTimeout(() => setAnimation("opacity-100 translate-x-0"), 50);
    }, 200);
  };

  return (
    <div className="w-full flex flex-col justify-center items-center bg-black p-5">
      <h2 className="text-3xl font-bold text-red-500">AutoSplitter Flags</h2>

      {/* Card Container */}
      <div className="relative w-full max-w-md mt-4">
        <div
          key={games[currentIndex].title}
          className={`w-full min-h-[250px] flex flex-col justify-center bg-black dark:bg-gray-800 rounded-xl shadow-lg p-6 text-center transition-all duration-700 transform ${animation}`}
          style={{
            backgroundImage: `url(${games[currentIndex].image})`,
            backgroundSize: "cover",
            backgroundPosition: "center",
            backgroundBlendMode: "overlay",
            backgroundColor: "rgba(0, 0, 0, 0.6)",
            backdropFilter: "blur(10px)",
          }}
        >
          <h2 className="text-xl font-bold text-yellow-600">
            {games[currentIndex].title}
          </h2>
          <p className="text-sm text-green-200 underline">
            {games[currentIndex].description}
          </p>
          <ul className="mt-2 text-sm text-white">
            {games[currentIndex].flags.map((flag, i) => (
              <li key={i} className="list-disc ml-4">
                {flag}
              </li>
            ))}
          </ul>
        </div>

        {/* Nav Buttons */}
        <div className="flex justify-between mt-2">
          <button
            onClick={() => changeCard("prev")}
            className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 transition"
          >
            ◀ Prev
          </button>
          <button
            onClick={() => changeCard("next")}
            className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 transition"
          >
            Next ▶
          </button>
        </div>
      </div>
    </div>
  );
};
