import { Toaster } from "sonner";
import "./App.css";
import { StoreProcess } from "./feature/store/storeProcess";

function App() {
  return (
    <>
      <Toaster
        toastOptions={{
          classNames: {
            description: "!text-red-900",
          },
        }}
      />
      <StoreProcess></StoreProcess>
    </>
  );
}

export default App;
