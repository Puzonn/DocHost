export interface ServerStatus {
  id: string;
  containerId: string;
  ownerId: number;
  ownerUsername: string;
  image: string;
  status: string;
  state: string;
  name: string;
  createdAt: Date;
  ports: Port[];
}

export interface Port {
  ip: string;
  privatePort: string;
  publicPort: string;
  type: string;
}

export interface MinecraftHostCreation {}

export interface Command {
  Content: string;
  IsServer: boolean;
}

export interface ContainerOption {
  containerName: string;
  memory: number;
  vCpus: number;
}

export interface User {
  username: string;
  userId: number;
  role: string;
  createdAt?: Date;
}

export interface UserContextType {
  isLoggedIn: boolean;
  user: User | undefined;
  setUser: React.Dispatch<React.SetStateAction<User | undefined>>;
}
