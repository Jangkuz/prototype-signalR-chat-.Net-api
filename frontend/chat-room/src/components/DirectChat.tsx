import React, { useState, useEffect } from "react";
import axios from "axios";
import { ROUTES } from "../config/routes";
import { ChatService } from "../services/chatService";
import { messageDTO } from "../models/messageDTO";
import { create } from "domain";

interface DirectChatProps {
  currentUserId: string;
  selectedUserId: string;
}

const DirectChat: React.FC<DirectChatProps> = ({
  currentUserId,
  selectedUserId,
}) => {
  const chatService = new ChatService();
  const [messages, setMessages] = useState<any[]>([]);
  const [message, setMessage] = useState("");
  const [currentRoomId, setRoomId] = useState<number>(0);
  const [isConnected, setIsConnected] = useState(false);

  // useEffect(() => {
  //   return () => {
  //     chatService
  //       .stop()
  //       .then(() => console.log("Connection stopped for partner change"));
  //   };
  // }, [currentRoomId]);

  useEffect(() => {
    // let isMounted = true;

    const initDirectChat = async () => {
      console.log("initDirectChat");
      try {
        await chatService.start();

        // if (!isMounted) return;

        setIsConnected(true);

        // Register the messge handler ONCE
        chatService.onReceiveMessage((message) => {
          // if (isMounted) {
          setMessages((prev) => [...prev, message]);
          // }
        });

        //Get or create chat room
        // Try to get existing room
        let response = await axios.get(
          ROUTES.API.CHAT.ROOMS.GET_BY_ACCOUNT_IDS(
            Number(currentUserId),
            Number(selectedUserId)
          )
        );

        // If no room exists, create one and add users
        if (!response.data) {
          const createRoom = {
            Acc1Id: Number(currentUserId),
            Acc2Id: Number(selectedUserId),
          };
          response = await axios.post(ROUTES.API.CHAT.ROOMS.CREATE, createRoom);

          console.log(response);
          // Add both users to room
          await axios.post(ROUTES.API.CHAT.ROOM_MEMBERS.ADD_USER, {
            RoomId: response.data.id,
            AccountId: currentUserId,
          });

          await axios.post(ROUTES.API.CHAT.ROOM_MEMBERS.ADD_USER, {
            RoomId: response.data.id,
            AccountId: selectedUserId,
          });
        }

        // if (!isMounted) return;
        const room = response.data;
        console.log("Current room id", room.id);
        setRoomId(room.id);

        // Join room via SignalR
        await chatService.joinRoom(room.id);

        // Load previous messages
        const messagesResponse = await axios.get(
          ROUTES.API.CHAT.MESSAGES.GET_BY_ROOM_ID(room.id)
        );
        console.log(messagesResponse);
        // if (!isMounted) return;

        setMessages(messagesResponse.data);
      } catch (error) {
        console.log("Failed to initialize chat:", error);
      }
    };

    if (currentUserId && selectedUserId) {
      initDirectChat();
    }

    // Clean up function: Stop connection and remove listeners
    // return () => {
    //   isMounted = false;
    //   chatService
    //     .stop()
    //     .then(() => console.log("Connection stoppped and cleanup"));
    // };
  }, [currentUserId, selectedUserId]);
  // }, [currentUserId, selectedUserId, isConnected]);

  const sendMessage = async () => {
    if (message.trim() && currentRoomId) {
      const messageData: messageDTO = {
        roomId: Number(currentRoomId),
        content: message,
        senderId: Number(currentUserId),
      };

      try {
        await axios.post(ROUTES.API.CHAT.MESSAGES.CREATE, messageData);

        // await chatService.sendMessage(roomId, message, Number(currentUserId));
        await chatService.sendMessage(messageData);
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
