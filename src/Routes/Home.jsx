import React from "react";
import { AutoSplitterFlags } from "../Components/Home/Cards";
import { Badges } from "../Components/Home/Badges";
import { Header } from "../Components/Home/Header";
import { Footer } from "../Components/Home/Footer";
import { Features } from "../Components/Home/Features";
import { FireParticles } from "../Components/Home/FireParticles";
import "./Home.css";

import { useNavigate } from "react-router-dom";

export const Home = () => {
  const navigate = useNavigate();
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
        
        <button
          className="max-w-[280px] bg-amber-500 text-white my-6 px-6 py-3 rounded-full text-lg font-semibold shadow-lg transition-all duration-300 hover:shadow-[0_0_25px_rgba(251,191,36,0.9)] hover:scale-105 hover:bg-amber-600 flex justify-center mx-auto"
          onClick={() => navigate("Download")}
        >
          Download Stable
        </button>

        <AutoSplitterFlags />

        <Features />
        <Footer />
      </div>
    </>
  );
};
