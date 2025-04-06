import React, { useState } from "react";
import { motion, AnimatePresence } from "framer-motion";
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
  const [selectedImage, setSelectedImage] = useState(null);

  return (
    <>
      <FireParticles />
      <div className="bg-black">
        <Header h2Valor={"Open Beta 3.0"} />
        {/* Visual Features Section */}
        <section
          id="visual-features"
          className="w-full py-16 px-4 flex flex-col items-center gap-12">
          {features.map((feature, index) => (
            <motion.div
              key={index}
              initial={{ opacity: 0, y: 40 }}
              whileInView={{ opacity: 1, y: 0 }}
              transition={{ duration: 0.8, ease: "easeOut" }}
              viewport={{ once: false, amount: 0.6 }}
              className="w-full max-w-2xl flex flex-col items-center text-center">
              <h2 className="text-white text-3xl md:text-5xl font-bold mb-4">
                {feature.text}
              </h2>
              <img
                src={`${import.meta.env.BASE_URL}assets/${feature.img}`}
                alt={`Feature ${index}`}
                className="w-full rounded-xl shadow-lg object-cover cursor-pointer"
                onClick={() =>
                  setSelectedImage(
                    `${import.meta.env.BASE_URL}assets/${feature.img}`
                  )
                }
              />
              <p className="text-[10px] text-yellow-400 mt-1 self-end">
                Click image to expand
              </p>
            </motion.div>
          ))}
        </section>

        {/* Lightbox for image preview */}
        <AnimatePresence>
          {selectedImage && (
            <motion.div
              className="fixed inset-0 z-50 bg-black/80 flex items-center justify-center p-4"
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
              onClick={() => setSelectedImage(null)}>
              <motion.img
                src={selectedImage}
                alt="Expanded"
                className="max-w-full max-h-full rounded-xl shadow-2xl"
                initial={{ scale: 0.8, opacity: 0 }}
                animate={{ scale: 1, opacity: 1 }}
                exit={{ scale: 0.8, opacity: 0 }}
                transition={{ duration: 0.3 }}
              />
            </motion.div>
          )}
        </AnimatePresence>

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
                className="inline-block text-white font-bold py-2 px-4 rounded-lg transition bg-black border border-white shadow-[0_0_15px_rgba(255,255,255,0.4)] hover:shadow-[0_0_25px_rgba(255,255,255,0.8)]">
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
                className="inline-block text-white font-bold m-1 py-2 px-4 rounded-lg transition bg-black border border-white shadow-[0_0_15px_rgba(255,255,255,0.4)] hover:shadow-[0_0_25px_rgba(255,255,255,0.8)]">
                Download HCMv2
              </a>
            </div>
          </div>
        </section>
      </div>
    </>
  );
};
