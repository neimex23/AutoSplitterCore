export const ThirdPartyNavbar = () => {
  // Función para hacer scroll hacia el componente específico
  const scrollToSection = (id) => {
    const section = document.getElementById(id);
    if (section) {
      section.scrollIntoView({ behavior: "smooth" });
    }
  };

  return (
    <nav className="bg-gray-800 text-white p-3 shadow-md w-full sticky top-0 z-50 rounded m-2">
      <div className="container mx-auto flex justify-center space-x-6">
        <button
          onClick={() => scrollToSection("intro")}
          className="hover:text-yellow-300 transition duration-500">
          Introduction
        </button>
        <button
          onClick={() => scrollToSection("dll-used")}
          className="hover:text-yellow-300 transition duration-500">
          API Info
        </button>
        <button
          onClick={() => scrollToSection("customs")}
          className="hover:text-yellow-300 transition duration-500">
          Custom DLLs
        </button>
        <button
          onClick={() => scrollToSection("resources")}
          className="hover:text-yellow-300 transition duration-500">
          Resources
        </button>
        <button
          onClick={() => scrollToSection("licenses")}
          className="hover:text-yellow-300 transition duration-500">
          Licenses
        </button>
      </div>
    </nav>
  );
};
