import { useEffect, useState } from "react";
import { motion } from "framer-motion";

export const FireParticles = () => {
  const [particles, setParticles] = useState([]);

  useEffect(() => {
    const newParticles = Array.from({ length: 40 }, (_, i) => ({
      id: i,
      x: Math.random() * window.innerWidth,
      y: Math.random() * window.innerHeight + window.scrollY,
      size: Math.random() * 5 + 2,
      duration: Math.random() * 3 + 2,
    }));
    setParticles(newParticles);
  }, []);

  return (
    <div className="fixed inset-0 overflow-hidden pointer-events-none">
      {particles.map((p) => (
        <motion.div
          key={p.id}
          initial={{ opacity: 0, y: p.y + 20 }}
          animate={{ opacity: [0, 1, 0], y: [p.y, p.y - 50] }}
          transition={{
            duration: p.duration,
            repeat: Infinity,
            repeatType: "loop",
            ease: "easeInOut",
          }}
          className="absolute bg-orange-500 rounded-full"
          style={{
            left: p.x,
            width: p.size,
            height: p.size,
            boxShadow: "0 0 10px rgba(255, 85, 0, 0.8)",
          }}
        />
      ))}
    </div>
  );
};