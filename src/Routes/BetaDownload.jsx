import React from "react";
import { motion } from "framer-motion";
import { FireParticles } from "../Components/Home/FireParticles";
import { Header } from "../Components/Home/Header";

const features = [
  {
    img: "1.jpg",
    text: "Experience a completely redesigned and modern interface.",
  },
  {
    img: "2.jpg",
    text: "Upload and share your profiles with the community.",
  },
  {
    img: "3.jpg",
    text: "New HitCounter: Automatically tracks hits in Celeste and Hollow Knight.",
  },
  {
    img: "4.jpg",
    text: "ASL Script support: Run community autosplitters directly in ASC.",
  },
  {
    img: "5.jpg",
    text: "Join the Open Beta and explore all the new features.",
  },
];

export const BetaDownload = () => {
  return (
    <>
      <FireParticles />
      <Header h2Valor={"ASC OpenBeta 3.0"} />
      <div className="bg-black">
        <section id="visual-features" className="w-full">
          {features.map((feature, index) => (
            <div
              key={index}
              className="relative w-full h-screen overflow-hidden"
            >
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
                className="relative w-full h-full flex items-center justify-center px-4"
              >
                <h2 className="text-white text-3xl md:text-5xl font-bold text-center bg-black/60 p-6 rounded-xl shadow-lg max-w-3xl">
                  {feature.text}
                </h2>
              </motion.div>
            </div>
          ))}
        </section>
      </div>
    </>
  );
};
