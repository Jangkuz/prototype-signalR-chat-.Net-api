import React, { useState, useEffect } from "react";
import axios from "axios";
import { ROUTES } from "../config/routes";

interface User {
  id: string;
  username: string;
  email: string;
}

interface UserSelectorProps {
  onSelectCurrentUser: (userId: string) => void;
  onSelectChatUser: (userId: string) => void;
  currentUserId: string;
}

const UserSelector: React.FC<UserSelectorProps> = ({
  onSelectCurrentUser,
  onSelectChatUser,
  currentUserId,
}) => {
  const [users, setUsers] = useState<User[]>([]);
  const [newUsername, setNewUsername] = useState("");
  const [newEmail, setNewEmail] = useState("");

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    const response = await axios.get(ROUTES.API.USERS.GET_ALL);
    setUsers(response.data);
  };

  const createUser = async () => {
    if (newUsername && newEmail) {
      await axios.post(ROUTES.API.USERS.CREATE, {
        username: newUsername,
        email: newEmail,
      });
      fetchUsers();
      setNewUsername("");
      setNewEmail("");
    }
  };

  return (
    <div>
      <div>
        <h3>Select Your User</h3>
        <select
          value={currentUserId}
          onChange={(e) => onSelectCurrentUser(e.target.value)}
        >
          <option value="">Select User</option>
          {users.map((user) => (
            <option key={user.id} value={user.id}>
              {user.username}
            </option>
          ))}
        </select>
      </div>

      {currentUserId && (
        <div>
          <h3>Select User to Chat With</h3>
          <select onChange={(e) => onSelectChatUser(e.target.value)}>
            <option value="">Select User</option>
            {users
              .filter((user) => user.id !== currentUserId)
              .map((user) => (
                <option key={user.id} value={user.id}>
                  {user.username}
                </option>
              ))}
          </select>
        </div>
      )}

      <div>
        <input
          placeholder="Username"
          value={newUsername}
          onChange={(e) => setNewUsername(e.target.value)}
        />
        <input
          placeholder="Email"
          value={newEmail}
          onChange={(e) => setNewEmail(e.target.value)}
        />
        <button onClick={createUser}>Create User</button>
      </div>
    </div>
  );
};

export default UserSelector;
