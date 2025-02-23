import React, { useState, useEffect } from "react";
import axios from "axios";
import { ROUTES } from "../config/routes";
import { ChatService } from "../services/chatService";

const chatService = new ChatService();

interface DirectChatProps {
  currentUserId: string;
  selectedUserId: string;
}

const DirectChat: React.FC<DirectChatProps> = ({
  currentUserId,
  selectedUserId,
}) => {
  const [messages, setMessages] = useState<any[]>([]);
  const [message, setMessage] = useState("");
  const [roomId, setRoomId] = useState<number>(0);
  const [isConnected, setIsConnected] = useState(false);

  useEffect(() => {
    const initDirectChat = async () => {
      try {
        if (!isConnected) {
          await chatService.start();
          setIsConnected(true);
          chatService.onReceiveMessage((message) => {
            setMessages((prev) => [...prev, message]);
          });
        }
        // Try to get existing room
        let response = await axios.get(
          ROUTES.API.CHAT.ROOM_MEMBERS.GET_BY_USER_ID(Number(currentUserId))
        );

        // If no room exists, create one and add users
        if (!response.data) {
          response = await axios.post(
            ROUTES.API.CHAT.ROOMS.CREATE(
              Number(currentUserId),
              Number(selectedUserId)
            )
          );

          // Add both users to room
          await axios.post(
            ROUTES.API.CHAT.ROOM_MEMBERS.ADD_USER(
              response.data.id,
              Number(currentUserId)
            )
          );
          await axios.post(
            ROUTES.API.CHAT.ROOM_MEMBERS.ADD_USER(
              response.data.id,
              Number(selectedUserId)
            )
          );
        }

        const room = response.data;
        setRoomId(room.id);

        // Join room via SignalR
        await chatService.joinRoom(room.id);

        // Load previous messages
        const messagesResponse = await axios.get(
          ROUTES.API.CHAT.MESSAGES.GET_BY_ROOM_ID(room.id)
        );
        setMessages(messagesResponse.data);
      } catch (error) {
        console.error("Failed to initialize chat:", error);
      }
    };

    if (currentUserId && selectedUserId) {
      initDirectChat();
    }
  }, [currentUserId, selectedUserId, isConnected]);

  const sendMessage = async () => {
    if (message.trim() && roomId) {
      const messageData = {
        roomId: Number(roomId),
        content: message,
        userId: Number(currentUserId),
      };

      try {
        await axios.post(ROUTES.API.CHAT.MESSAGES.CREATE, messageData);

        // await chatService.sendMessage(roomId, message, Number(currentUserId));
        await chatService.sendMessage2(messageData);
        setMessage("");
      } catch (error) {
        console.error("Failed to send message:", error);
      }
    }
  };

  return (
    <div>
      <div>
        {messages.map((msg, index) => (
          <div
            key={index}
            className={`message ${
              msg.userId === currentUserId ? "sent" : "received"
            }`}
          >
            <div>
              User {msg.userId} says: {msg.content}
            </div>
          </div>
        ))}
      </div>
      <input
        value={message}
        onChange={(e) => setMessage(e.target.value)}
        onKeyPress={(e) => e.key === "Enter" && sendMessage()}
      />
      <button onClick={sendMessage}>Send</button>
    </div>
  );
};

export default DirectChat;
