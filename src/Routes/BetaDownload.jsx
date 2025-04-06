import React from "react";
import { motion } from "framer-motion";
import { FireParticles } from "../Components/Home/FireParticles";
import { Header } from "../Components/Home/Header";

const features = [
  {
    img: "1.png",
    text: "Experience a completely redesigned and modern interface.",
  },
  {
    img: "2.png",
    text: "Upload and share your profiles with the community.",
  },
  {
    img: "3.png",
    text: "New HitCounter: Automatically tracks hits in Celeste and Hollow Knight.",
  },
  {
    img: "4.png",
    text: "Multi-select mode to select flags more easily",
  },
  {
    img: "5.png",
    text: "ASL Script support: Run community autosplitters directly in ASC.",
  },
];

export const BetaDownload = () => {
  return (
    <>
      <FireParticles />
      <Header h2Valor={"Open Beta 3.0"} />
      <div className="bg-black">
        <section id="visual-features" className="w-full">
          {features.map((feature, index) => (
            <div
              key={index}
              className="relative w-full h-screen overflow-hidden">
              <img
                src={`${import.meta.env.BASE_URL}assets/${feature.img}`}
                alt={`Feature ${index}`}
                className="fixed inset-0 w-full h-full object-cover -z-10"
              />
              <motion.div
                initial={{ opacity: 0, y: 40 }}
                whileInView={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.8 }}
                viewport={{ once: true }}
                className="relative w-full h-full flex items-center justify-center px-4">
                <h2 className="text-white text-3xl md:text-5xl font-bold text-center bg-black/60 p-6 rounded-xl shadow-lg max-w-3xl">
                  {feature.text}
                </h2>
              </motion.div>
            </div>
          ))}
        </section>

        {/* Download Section */}
        <section
          id="downloads"
          className="w-full py-16 px-4 flex flex-col items-center gap-12">
          <h2 className="text-white text-4xl font-bold text-center">
            Download ASC 3.0 Open Beta
          </h2>
          <h3 className="text-white font-bold text-center">Beta version: 1</h3>
          <div className="flex flex-col md:flex-row gap-8 justify-center items-stretch max-w-5xl w-full">
            <div className="bg-white/5 backdrop-blur-md p-6 rounded-2xl shadow-lg flex-1 text-center">
              <h3 className="text-white text-2xl font-semibold mb-2">HCMv1</h3>
              <p className="text-white/80 mb-4 m-4">
                Try the new ASC version using the classic HitCounterManager.
              </p>
              <a
                href="https://www.mediafire.com/file/frx99siyfdli8lx/ASCv3.0-HCMv1_Beta1.zip/file"
                target="_blank"
                rel="noopener noreferrer"
                className="inline-block text-white font-bold py-2 px-4 rounded-lg transition 
             bg-black border border-white shadow-[0_0_15px_rgba(255,255,255,0.4)]
             hover:shadow-[0_0_25px_rgba(255,255,255,0.8)]">
                Download HCMv1
              </a>
            </div>
            <div className="bg-white/5 backdrop-blur-md p-6 rounded-2xl shadow-lg flex-1 text-center">
              <h3 className="text-white text-2xl font-semibold mb-2 m-2">
                HCMv2
              </h3>
              <p className="text-white/80 mb-4">
                Try the new ASC version with the redesigned HitCounterManager
                UI.
              </p>
              <a
                href="https://www.mediafire.com/file/bwokvmkwjmm1251/ASCv3.0-HCMv2_Beta1.zip/file"
                target="_blank"
                rel="noopener noreferrer"
                className="inline-block text-white font-bold m-1 py-2 px-4 rounded-lg transition 
             bg-black border border-white shadow-[0_0_15px_rgba(255,255,255,0.4)]
             hover:shadow-[0_0_25px_rgba(255,255,255,0.8)]">
                Download HCMv2
              </a>
            </div>
          </div>
        </section>
      </div>
    </>
  );
};
