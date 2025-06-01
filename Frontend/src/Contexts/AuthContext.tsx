import { createContext } from "react";
import type { UserContextType } from "../Types/Types";

export const AuthContext = createContext<UserContextType>({
  user: { userId: 0, username: "" },
  setUser: () => {},
  isLoggedIn: false,
});
