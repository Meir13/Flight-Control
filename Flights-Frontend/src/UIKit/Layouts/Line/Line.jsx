import "./Line.css";

export const Line = ({ children, addClass }) => {
  return <div className={`Line ${addClass || ""}`}>{children}</div>;
};

export const Between = (props) => {
  return <Line {...props} addClass={"between"} />;
};

export const Rows = (props) => {
  return <Line {...props} addClass="rows" />;
};

export const Wide = (props) => {
  return <Line {...props} addClass="wide" />;
};

export const LineHeader = (props) => {
  return <Line {...props} addClass="leg-header" />;
};
