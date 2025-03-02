// src/models/Message.ts
export class messageDTO {
  roomId: number;
  content: string;
  senderId: number;

  constructor(data?: Partial<messageDTO>) {
    this.roomId = data?.roomId || 0;
    this.content = data?.content || "";
    this.senderId = data?.senderId || 0;
  }
}
