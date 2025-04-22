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
        <Header h2Valor={"Open Beta 3.0"} />
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
          className="w-full py-16 px-4 flex flex-col items-center gap-12">
          <h2 className="text-white text-4xl font-bold text-center">
            Download ASC 3.0 Open Beta
          </h2>
          <h3 className="text-white font-bold text-center">Beta version: 2</h3>
          <div className="flex flex-col md:flex-row gap-8 justify-center items-stretch max-w-5xl w-full">
            <div className="bg-white/5 backdrop-blur-md p-6 rounded-2xl shadow-lg flex-1 text-center">
              <h3 className="text-white text-2xl font-semibold mb-2">HCMv1</h3>
              <p className="text-white/80 mb-4 m-4">
                Try the new ASC version using the classic HitCounterManager.
              </p>
              <a
                href="https://www.mediafire.com/file/u0ky6w2zsj59j7s/ASCv3.0-HCMv1_Beta2.zip/file"
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
                href="https://www.mediafire.com/file/mjkyrc91vwe88ol/ASCv3.0-HCMv2_Beta2.zip/file"
                target="_blank"
                rel="noopener noreferrer"
                className="inline-block text-white font-bold m-1 py-2 px-4 rounded-lg transition bg-black border border-white shadow-[0_0_15px_rgba(255,255,255,0.4)] hover:shadow-[0_0_25px_rgba(255,255,255,0.8)]">
                Download HCMv2
              </a>
            </div>
          </div>
        </section>

        <section
          id="migration"
          className="w-full py-16 px-4 flex flex-col items-center gap-8">
          <h2 className="text-white text-4xl font-bold text-center">
            Migration from ASC 1.14 and earlier
          </h2>
          <div className="max-w-4xl text-white/90 text-lg text-center bg-white/5 backdrop-blur-md p-6 rounded-2xl shadow-lg">
            <p className="mb-4">
              You can migrate your data from ASC 1.14 or earlier by renaming the
              file:
            </p>
            <code className="bg-black/60 text-yellow-300 p-2 rounded block my-2">
              HitCounterManagerSaveAutoSplitter.xml
            </code>
            <p className="mb-4">to:</p>
            <code className="bg-black/60 text-yellow-300 p-2 rounded block my-2">
              SaveDataAutoSplitter.xml
            </code>
            <p className="mt-6">
              Additionally, all stylesheets, splits, and configuration data
              stored in
              <code className="text-white px-2">
                HitCounterManagerSave.xml
              </code>{" "}
              — for both HCMv1 and HCMv2 — are fully compatible with the current
              beta version.
            </p>
            <p className="mt-6">
              To Update new beta version just remplace your folder with new
              files
            </p>
          </div>
        </section>
      </div>
    </>
  );
};
