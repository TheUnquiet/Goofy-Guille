import { GoogleLogin } from "@react-oauth/google";

function App() {
  return (
    <main>
      <h1>BasketBall Club</h1>
      <p>Sign in with Google</p>
      <GoogleLogin 
        onSuccess={(response) => { console.log("Google ID token:", response.credential); }}
        onError={() => { console.log("Google sign-in failed");}}
      />
    </main>
  )
}

export default App;