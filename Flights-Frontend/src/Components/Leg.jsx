import "./Leg.css";
import axios from "axios";
import { useState } from "react";
import { useEffect } from "react";
import { Flight } from "./Flight";
import { default as getInstance } from "../signalr-connector.ts";
import { Grid, LegGrid } from "../UIKit/Layouts/Grid/Grid";
import { Between, Line, LineHeader } from "../UIKit/Layouts/Line/Line";
// import Connector from "../signalr-connector.ts";

export const Leg = () => {
  let flightProps = {};
  let isFirstFlight = true;
  const [legs, setLegs] = useState([]);
  const connector = getInstance();

  const URL = "https://localhost:7293/api/Flights/legs";
  useEffect(() => {
    async function getLegs() {
      try {
        const response = await axios.get(URL);
        setLegs(response.data);
      } catch (error) {
        console.error(error);
      }
    }

    getLegs();
  }, []);

  useEffect(() => {
    const updatedLegs = [...legs];
    if (isFirstFlight && legs.length > 0 && legs[0].flight) {
      isFirstFlight = false;
      flightProps = legs[0].flight;
    }

    connector.onFlightAdded = (flight) => {
      console.log("Flight added", flight);
      const legIndexToDelete = legs.findIndex(
        (l) => l.flight && l.flight.id === flight.id
      );
      if (legIndexToDelete !== -1) updatedLegs[legIndexToDelete].flight = null;
      const legIndex = legs.findIndex((l) => l.legNumber === flight.currentLeg);
      updatedLegs[legIndex].flight = flight;
      setLegs(updatedLegs);
    };

    connector.onFlightRemove = (id) => {
      console.log("removing");
      const legIndexToDelete = legs.findIndex(
        (l) => l.flight && l.flight.id === id
      );

      if (legIndexToDelete !== -1) updatedLegs[legIndexToDelete].flight = null;
      setLegs(updatedLegs);
    };
  }, [legs]);

  return (
    <LegGrid>
      <div className="leg-header">Leg</div>
      <div className="leg-header">Type</div>
      <div className="leg-header">Flight</div>
      <div className="leg-header">Passengers</div>
      <div className="leg-header">Brand</div>
      <div className="leg-header">Landing</div>
      <div className="leg-header">Status</div>
      <div className="leg-header">Pilot</div>

      {legs.map((leg, i) => (
        <div key={leg.id} className="leg" style={{ gridRow: i + 2 }}>
          <div>{leg.legNumber}</div>
          <div className="marg-left" style={{ gridColumn: 2 }}>
            {leg.legType}
          </div>
          {leg.flight && <Flight flight={leg.flight}></Flight>}
        </div>
      ))}
    </LegGrid>
  );
};
