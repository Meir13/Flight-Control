import * as signalR from "@microsoft/signalr";
const URL = process.env.HUB_ADDRESS ?? "https://localhost:7293/flighthub";

class Connector {
  private connection: signalR.HubConnection;
  public onFlightAdded: (flight) => {};
  public onFlightRemove: (id) => {};
  static instance: Connector;

  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(URL)
      .withAutomaticReconnect()
      .build();

    this.connection.start().catch((err) => document.write(err));

    this.connection.on("flightAdded", (flight) => {
      this.onFlightAdded(flight);
    });

    this.connection.on("removeFlight", (id) => {
      this.onFlightRemove(id);
    });
  }

  public static getInstance(): Connector {
    if (!Connector.instance) Connector.instance = new Connector();
    return Connector.instance;
  }
}
export default Connector.getInstance;
