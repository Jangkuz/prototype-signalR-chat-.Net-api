import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
} from "@microsoft/signalr";
import { ROUTES } from "../config/routes";
import { messageDTO } from "../models/messageDTO";
export class ChatService {
  private connection: HubConnection;

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl(ROUTES.API.CHAT.HUB)
      .withAutomaticReconnect()
      .build();

    this.connection.onclose((error) =>
      console.log("Connection closed: ", error)
    );

    this.connection.onreconnecting((error) =>
      console.log("Reconnecting: ", error)
    );

    this.connection.onreconnected(() => console.log("Reconnected!"));
  }

  async start() {
    //Check conneciton state
    if (this.connection.state === HubConnectionState.Connected) {
      console.log("Connection already started.");
      return;
    }

    //Connect SignalR
    try {
      console.log("Starting connection....");
      await this.connection.start();
      console.log(
        "Connection started successfully:",
        this.connection.connectionId
      );
    } catch (error) {
      console.log("Failed to start connection: ", error);
      throw error;
    }
  }

  async stop() {
    try {
      console.log("Stopping connection...");
      await this.connection.stop();
      console.log("Connection stopped successfully.");
    } catch (error) {
      console.error("Failed to stop connection:", error);
      throw error;
    }
  }

  private async ensureConnected() {
    if (this.connection.state !== HubConnectionState.Connected) {
      //> keep reconnect again if not connected
      await this.start();
    }
  }

  async joinRoom(roomId: number) {
    // check if the client already connect or not
    await this.ensureConnected();
    console.log(
      "Joining room:",
      roomId,
      "Connection ID:",
      this.connection.connectionId
    );

    try {
      await this.connection.invoke("JoinRoom", roomId);
      console.log("Successfully joined room:", roomId);
    } catch (error) {
      console.error("Failed to join room:", error);
      throw error;
    }
  }

  // async sendMessage(roomId: number, message: string, userId: number) {
  //   console.log("Sending message to room:", roomId, message, userId);
  //   await this.connection.invoke("SendMessage", roomId, message, userId);
  // }

  async sendMessage(messageDTO: messageDTO) {
    await this.ensureConnected();
    console.log(
      "Sending message:",
      messageDTO,
      "Connection ID:",
      this.connection.connectionId
    );

    try {
      await this.connection.invoke("SendMessage", messageDTO);
      console.log("Message sent successfully!");
    } catch (error) {
      console.error("Failed to send message:", error);
      throw error;
    }
  }

  onReceiveMessage(callback: (message: messageDTO) => void) {
    console.log("Setting up message receiver...");
    this.connection.on("ReceiveMessage", (message) => {
      console.log("Received message:", message);
      callback(message);
    });
  }
}
