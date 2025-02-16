import React from "react";
import ReactDOM from "react-dom/client";
import { HashRouter } from "react-router-dom";
import { createRoot } from "react-dom/client";
import App from "./App";

import "tailwindcss";

createRoot(document.getElementById("root")).render(
  <HashRouter>
    <App />
  </HashRouter>
);
