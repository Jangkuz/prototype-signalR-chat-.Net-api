import React, { useState } from "react";
import "./App.css";
import UserSelector from "./components/UserSelector";
import DirectChat from "./components/DirectChat";

function App() {
  const [currentUserId, setCurrentUserId] = useState<string>("");
  const [selectedUserId, setSelectedUserId] = useState<string>("");

  return (
    <div className="App">
      <UserSelector
        onSelectCurrentUser={(userId) => setCurrentUserId(userId)}
        onSelectChatUser={(userId) => setSelectedUserId(userId)}
        currentUserId={currentUserId}
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
