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


export interface Command{
    Content: string;
    IsServer: boolean;
}