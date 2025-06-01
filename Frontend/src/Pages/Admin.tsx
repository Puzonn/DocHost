import { ContainerCreator } from "../Components/ContainerCreator";
import { ServerList } from "../Components/ServerList";

export const Admin = () => {
  return (
    <>
      <ServerList></ServerList>
      <ContainerCreator></ContainerCreator>
    </>
  );
};
