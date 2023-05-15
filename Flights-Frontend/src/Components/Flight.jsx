import { Line } from "../UIKit/Layouts/Line/Line";

export const Flight = ({ flight }) => {
  console.log("entered flight comp");
  if (flight == null) return <></>;
  return (
    <>
      <p> {flight.number}</p>
      <p> {flight.passengerCount}</p>
      <p> {flight.brand}</p>
      <p> {flight.landingTime}</p>
      <p> {flight.status}</p>
      <p> {flight.pilotName}</p>
    </>
  );
};
