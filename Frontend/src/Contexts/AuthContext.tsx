import { createContext } from "react";
import type { UserContextType } from "../Types/Types";

export const AuthContext = createContext<UserContextType | undefined>(
  undefined
);
