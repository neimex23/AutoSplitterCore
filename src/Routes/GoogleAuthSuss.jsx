import React from "react";
import { Header } from "../Components/Home/Header";
import { Footer } from "../Components/Home/Footer";
import { FireParticles } from "../Components/Home/FireParticles";

export const GoogleAuthSuss = () => {
  return (
    <>
      <Header />
      <FireParticles />
      <div className="bg-black min-h-screen flex items-center justify-center text-center px-4">
        <div className="bg-white text-black rounded-lg shadow-lg p-6 max-w-lg w-full">
          <h2 className="text-3xl font-bold mb-4">Authentication Complete</h2>
          <p className="text-lg mb-6">You can now close this window.</p>
        </div>
      </div>
      <Footer />
    </>
  );
};
