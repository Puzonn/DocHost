export interface ContainerStatus { 
    Id: string;
    Status: string;
    State: string;
    Name: string;
    CreatedAt: Date;
    Ports: Port[]
}

export interface Port {
    IP: string;
    PrivatePort: string;
    PublicPort: string;
    Type: string;
}

export interface MinecraftHostCreation {
    
}