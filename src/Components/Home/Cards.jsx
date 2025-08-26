import { image } from "framer-motion/client";
import React, { useEffect, useMemo, useState } from "react";

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
      "https://m.media-amazon.com/images/I/81HFdlL5MIL._UF1000,1000_QL80_.jpg",
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
    image: "https://easycdn.es/1/v/video_34812.jpg",
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
    image: "https://i.blogs.es/442f82/020119-celeste/1366_2000.jpg",
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
  {
    title: "ASL Scripting",
    description: "Split whatreaver game that have a asl with autosplitter",
    flags: [],
    image:
      "https://www.shutterstock.com/image-vector/gamer-joypad-console-controller-gamepad-600nw-2274436813.jpg",
  },
];

export const AutoSplitterFlags = () => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const [animation, setAnimation] = useState("");
  const [imgLoaded, setImgLoaded] = useState(false);

  const hasData = games && games.length > 0;
  const game = hasData ? games[currentIndex] : null;

  // Preload next & prev
  useEffect(() => {
    if (!hasData) return;
    const next = (currentIndex + 1) % games.length;
    const prev = (currentIndex - 1 + games.length) % games.length;
    [next, prev].forEach((idx) => {
      const i = new Image();
      i.src = games[idx].image;
    });
  }, [currentIndex, games, hasData]);

  const changeCard = (direction) => {
    if (!hasData) return;

    setAnimation(
      direction === "next"
        ? "translate-x-10 opacity-0"
        : "-translate-x-10 opacity-0"
    );

    setTimeout(() => {
      setCurrentIndex((prev) =>
        direction === "next"
          ? (prev + 1) % games.length
          : (prev - 1 + games.length) % games.length
      );
      setImgLoaded(false);
      setAnimation("opacity-0");
      setTimeout(() => setAnimation("opacity-100 translate-x-0"), 50);
    }, 200);
  };

  return (
    <div className="w-full flex flex-col justify-center items-center bg-black p-5">
      <h2 className="text-3xl font-bold text-red-500">AutoSplitter Flags</h2>

      <div className="relative w-full max-w-md mt-4">
        {!hasData && (
          <div className="w-full min-h-[300px] rounded-xl bg-gray-900 text-white grid place-items-center">
            <p>No data available.</p>
          </div>
        )}

        {hasData && (
          <div
            key={game.title}
            className={`relative w-full rounded-xl shadow-lg overflow-hidden transition-all duration-800 transform ${animation}`}
          >
            <div className="relative w-full aspect-[3/2]">
              {/* Imagen de fondo */}
              <img
                src={game.image}
                alt={game.title}
                loading="lazy"
                decoding="async"
                fetchPriority={currentIndex === 0 ? "high" : "low"}
                onLoad={() => setImgLoaded(true)}
                className={`absolute inset-0 w-full h-full object-cover transition-opacity duration-500 ${
                  imgLoaded ? "opacity-100" : "opacity-0"
                }`}
              />

              <div className="absolute inset-0 bg-black/60 backdrop-blur-[1px]" />

              <div className="absolute inset-0 z-10 flex flex-col items-center justify-center p-6 text-center text-white">
                <h3 className="text-2xl font-bold text-yellow-400 drop-shadow">
                  {game.title}
                </h3>
                <p className="text-sm text-green-200 underline mt-1">
                  {game.description}
                </p>

                <ul className="mt-3 text-sm space-y-1 overflow-auto text-left list-disc px-6 w-full max-w-[80%]">
                  {game.flags?.map((flag, i) => (
                    <li key={i}>{flag}</li>
                  ))}
                </ul>
              </div>       
            </div>
          </div>
        )}     
      </div>
      <div className="flex gap-4 m-3">
        <button
          onClick={() => changeCard("prev")}
          className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 transition disabled:opacity-50"
          disabled={!hasData}
        >
          ◀ Prev
        </button>
        <button
          onClick={() => changeCard("next")}
          className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 transition disabled:opacity-50"
          disabled={!hasData}
        >
          Next ▶
        </button>
      </div>
    </div>
  );
};