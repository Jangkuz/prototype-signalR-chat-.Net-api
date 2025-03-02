import React, { useState } from "react";
import "./App.css";
import UserSelector from "./components/UserSelector";
import DirectChat from "./components/DirectChat";

function App() {
  const [currentUserId, setCurrentUserId] = useState<string>("");
  const [selectedUserId, setSelectedUserId] = useState<string>("");

  const handleSelectCurrentUser = (userId: string) => {
    console.log("Setting current user to:", userId);
    setCurrentUserId(userId);
  };

  const handleSelectSelectedUser = (userId: string) => {
    console.log("Setting selected user to:", userId);
    setSelectedUserId(userId);
  };
  return (
    <div className="App">
      <UserSelector
        onSelectCurrentUser={handleSelectCurrentUser}
        onSelectChatUser={handleSelectSelectedUser}
        currentUserIdTemp={currentUserId}
      />

      {currentUserId && selectedUserId && (
        <DirectChat
          currentUserId={currentUserId}
          selectedUserId={selectedUserId}
        />
      )}
    </div>
  );
}

export default App;
