import "./Header.css";

import { useNavigate } from "react-router-dom";

export const Header = ({ h2Valor }) => {
  const navigate = useNavigate();

  return (
    <div className="mx-auto flex items-center justify-between gap-x-4 rounded-xl bg-white p-6 shadow-lg outline outline-black/5 dark:bg-slate-800 dark:shadow-none dark:-outline-offset-1 dark:outline-white/10">
      <header className="flex flex-col items-center flex-1">
        <h1 className="text-3xl text-white font-bold">AutoSplitterCore</h1>
        <h2 className="text-lg text-yellow-500 dark:text-yellow-500">
          {h2Valor}
        </h2>
      </header>

      <img
        className="size-16 shrink-0 ml-auto transition-all duration-300 ease-in-out transform hover:scale-110 hover:brightness-150 animate-fire-glow cursor-pointer"
        src="https://raw.githubusercontent.com/neimex23/AutoSplitterCore/refs/heads/master/Images/AutoSplitterSetupIcon.ico"
        alt="AutoSplitterCore Logo"
        onClick={() => navigate("/")}
      />
    </div>
  );
};
