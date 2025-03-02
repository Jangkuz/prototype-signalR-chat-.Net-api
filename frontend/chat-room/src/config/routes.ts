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
        CREATE:`${API_BASE}/api/chatrooms`,
        GET_BY_ID: (id: number) => `${API_BASE}/api/chatrooms/${id}`,
        GET_BY_ACCOUNT_IDS: (acc1Id: number, acc2Id: number) => `${API_BASE}/api/chatrooms/${acc1Id}/${acc2Id}`,
      },
      MESSAGES: {
        CREATE: `${API_BASE}/api/message`,
        GET_BY_ID: (id: number) => `${API_BASE}/api/message/${id}`,
        GET_BY_ROOM_ID: (roomId: number) =>
          `${API_BASE}/api/message/${roomId}/messages`,
      },
      ROOM_MEMBERS: {
        GET_BY_ACCOUNT_ID: (account1: number) =>
          `${API_BASE}/api/RoomMember/${account1}`,
        CREATE: `${API_BASE}/api/RoomMember`,
        ADD_USER: `${API_BASE}/api/RoomMember`,
      },
      HUB: `${API_BASE}/chatHub`,
    },
  },
} as const;
