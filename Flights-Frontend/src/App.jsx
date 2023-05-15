import { GridMain } from "./UIKit/Layouts/Grid/Grid";
import "./App.css";

export const App = ({ children }) => {
  return (
    <div className="App">
      <GridMain>
        <div>
          <h1>Flights Control</h1>
        </div>
        <div>{children}</div>
        <div className="bottom">
          <span className="pad-r">Meir Chechik Project</span>
        </div>
      </GridMain>
    </div>
  );
};
