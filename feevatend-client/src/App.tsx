import Login from "./pages/Login";
import Register from "./pages/Register";

function App() {
  const isRegister = window.location.pathname === "/register";
  return isRegister ? <Register /> : <Login />;
}

export default App;
