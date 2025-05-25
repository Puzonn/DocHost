import { ContainerCreator } from "./Components/ContainerCreator";
import { ContainerList } from "./Components/ContainersList";

export const Admin = () => {
  return (
    <>
      <ContainerList></ContainerList>
      <ContainerCreator></ContainerCreator>
    </>
  );
};
