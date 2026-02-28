import React, { useState, useEffect, useRef } from "react";
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
    text: "Upload, share and manage your profiles with the community.",
  },
  {
    img: "3.png",
    text: "HitCounter: Automatically tracks hits in Celeste and Hollow Knight.",
  },
  {
    img: "4.png",
    text: "Intuitive Selections Flags",
  },
  {
    img: "5.png",
    text: "ASL Script support: Run community livesplit autosplitters directly in ASC.",
  },
];

export const Download = () => {
  const [selectedImage, setSelectedImage] = useState(null);
  const [showScrollButton, setShowScrollButton] = useState(true);
  const afterHeaderRef = useRef(null);
  const downloadRef = useRef(null);

  useEffect(() => {
    const headerObserver = new IntersectionObserver(
      ([entry]) => {
        setShowScrollButton(!entry.isIntersecting);
      },
      { threshold: 0 }
    );

    const downloadObserver = new IntersectionObserver(
      ([entry]) => {
        if (entry.isIntersecting) {
          setShowScrollButton(false);
        }
      },
      { threshold: 0.5 }
    );

    if (afterHeaderRef.current) headerObserver.observe(afterHeaderRef.current);
    if (downloadRef.current) downloadObserver.observe(downloadRef.current);

    return () => {
      if (afterHeaderRef.current)
        headerObserver.unobserve(afterHeaderRef.current);
      if (downloadRef.current) downloadObserver.unobserve(downloadRef.current);
    };
  }, []);

  function smoothScrollTo(targetY, duration = 800) {
    const startY = window.scrollY;
    const diff = targetY - startY;
    let startTime = null;

    function easeInOutQuad(t) {
      return t < 0.5 ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }

    function animateScroll(currentTime) {
      if (startTime === null) startTime = currentTime;
      const time = currentTime - startTime;
      const percent = Math.min(time / duration, 1);
      const eased = easeInOutQuad(percent);
      window.scrollTo(0, startY + diff * eased);

      if (time < duration) {
        requestAnimationFrame(animateScroll);
      }
    }

    requestAnimationFrame(animateScroll);
  }

  return (
    <>
      <FireParticles />
      <div className="bg-black">
        <Header h2Valor={"Download AutoSplitterCore"} />
        <div ref={afterHeaderRef} className="w-full h-1" />
        {showScrollButton && (
          <div className="fixed top-4 right-4 z-50">
            <button
              onClick={() => {
                const el = document.getElementById("downloads");
                if (el) {
                  const y = el.getBoundingClientRect().top + window.scrollY;
                  smoothScrollTo(y);
                }
              }}
              className="bg-yellow-500 hover:bg-yellow-600 text-black font-bold py-2 px-4 rounded-full shadow-lg transition duration-300">
              Go to Downloads ↓
            </button>
          </div>
        )}

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
        ref={downloadRef}
        className="w-full py-24 px-6 flex items-center justify-center">

        <div className="max-w-4xl w-full bg-white/5 backdrop-blur-lg border border-white/10 rounded-3xl shadow-2xl p-12 text-center space-y-10">

          <h2 className="text-4xl md:text-5xl font-extrabold text-white tracking-wide">
            Download AutoSplitterCore
          </h2>

          <a
            href="https://github.com/neimex23/AutoSplitterCore/releases/latest"
            target="_blank"
            rel="noopener noreferrer"
            className="inline-block text-white font-bold text-xl px-12 py-5 rounded-2xl transition-all duration-300 bg-black border border-white shadow-[0_0_20px_rgba(255,255,255,0.5)] hover:shadow-[0_0_35px_rgba(255,255,255,0.9)] hover:scale-110 active:scale-95">
            Go to GitHub Releases
          </a>

        </div>
      </section>
      </div>
    </>
  );
};
