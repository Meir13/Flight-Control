import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { App } from "./App";
import { Leg } from "./Components/Leg";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <App>
    <Leg></Leg>
  </App>
);
