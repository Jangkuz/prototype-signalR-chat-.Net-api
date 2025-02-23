import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { ROUTES } from "../config/routes";
export class ChatService {
  private connection: HubConnection;

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl(ROUTES.API.CHAT.HUB)
      .withAutomaticReconnect()
      .build();
  }

  async start() {
    await this.connection.start();
  }

  async joinRoom(roomId: number) {
    await this.connection.invoke("JoinRoom", roomId);
  }

  async sendMessage(roomId: number, message: string, userId: number) {
    console.log("Sending message to room:", roomId, message, userId);
    await this.connection.invoke("SendMessage", roomId, message, userId);
  }

  async sendMessage2(messageDTO: any) {
    await this.connection.invoke("SendMessage2", messageDTO);
  }

  onReceiveMessage(callback: (message: any) => void) {
    console.log("Receiving message:", callback);
    this.connection.on("ReceiveMessage", callback);
  }
}
