import "./App.css";
import { StoreProcess } from "./feature/store/storeProcess";
import { Toaster } from 'react-hot-toast';

function App() {
  return (
    <>
       <Toaster  />
      <StoreProcess></StoreProcess>
    </>
  );
}

export default App;
