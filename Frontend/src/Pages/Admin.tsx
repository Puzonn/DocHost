import { ContainerCreator } from "../Components/ContainerCreator";
import { ServerList } from "../Components/ServerList";
import { UserList } from "../Components/UserList";

export const Admin = () => {
  return (
    <div className="flex flex-col gap-5">
      <ServerList></ServerList>
      <UserList></UserList>
      <ContainerCreator></ContainerCreator>
    </div>
  );
};
