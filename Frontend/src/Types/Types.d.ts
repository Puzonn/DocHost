export interface ContainerStatus {
  id: string;
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
}

export interface UserContextType {
  user: User | undefined;
  setUser: React.Dispatch<React.SetStateAction<User | undefined>>;
}
