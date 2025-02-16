import React from "react";
import { AutoSplitterFlags } from "../Components/Home/Cards";
import { Badges } from "../Components/Home/Badges";
import { Header } from "../Components/Home/Header";
import { Footer } from "../Components/Home/Footer";
import { Features } from "../Components/Home/Features";
import { FireParticles } from "../Components/Home/FireParticles";
import "./Home.css";

export const Home = () => {
  return (
    <>
      <FireParticles />
      <div className="bg-black">
        <Header h2Valor={"Extension for HitCounterManager"} />
        <Badges />
        <div className="flex justify-center items-center p-4">
          <img
            className="max-w-full h-auto rounded-lg shadow-lg"
            src="https://raw.githubusercontent.com/neimex23/HitCounterManager/master/Images/Wiki/AutoSplitterCore.png"
            alt="AutoSplitterCore"
          />
        </div>

        <a
          href="https://github.com/neimex23/AutoSplitterCore/releases/latest"
          target="_blank"
          className="max-w-[280px] bg-blue-600 text-white px-6 py-3 rounded-full text-lg font-semibold shadow-lg transition duration-300 hover:shadow-[0_0_20px_rgba(59,130,246,0.8)] hover:bg-blue-700 flex justify-center block mx-auto">
          Download Now
        </a>

        <AutoSplitterFlags />

        <Features />
        <Footer />
      </div>
    </>
  );
};
