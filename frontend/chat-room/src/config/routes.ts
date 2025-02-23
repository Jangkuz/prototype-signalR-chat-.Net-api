const API_BASE = "http://localhost:5022";

export const ROUTES = {
  API: {
    USERS: {
      GET_ALL: `${API_BASE}/api/users`,
      CREATE: `${API_BASE}/api/users`,
      GET_BY_ID: (id: number) => `${API_BASE}/api/users/${id}`,
    },
    CHAT: {
      ROOMS: {
        GET_ALL: `${API_BASE}/api/chatrooms`,
        CREATE: (RoomId: number, UserId: number) =>
          `${API_BASE}/api/chatrooms/${RoomId}/${UserId}`,
        GET_BY_ID: (id: number) => `${API_BASE}/api/chatrooms/${id}`,
      },
      MESSAGES: {
        CREATE: `${API_BASE}/api/message`,
        GET_BY_ID: (id: number) => `${API_BASE}/api/message/${id}`,
        GET_BY_ROOM_ID: (roomId: number) =>
          `${API_BASE}/api/message/${roomId}/messages`,
      },
      ROOM_MEMBERS: {
        GET_BY_USER_ID: (userId: number) =>
          `${API_BASE}/api/RoomMember/${userId}`,
        CREATE: `${API_BASE}/api/RoomMember`,
        ADD_USER: (roomId: number, userId: number) =>
          `${API_BASE}/api/RoomMember/${roomId}/${userId}`,
      },
      HUB: `${API_BASE}/chatHub`,
    },
  },
} as const;
