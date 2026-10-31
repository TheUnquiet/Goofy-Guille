import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { GoogleOAuthProvider } from '@react-oauth/google'
import { AuthProvider } from './auth/AuthContext.tsx'
import { BrowserRouter } from "react-router-dom";

const googleClientId = "1034658964004-mlnn0f9q4ihflu4fb338ganpus0se11h.apps.googleusercontent.com"

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
     <GoogleOAuthProvider clientId={googleClientId}>
       <AuthProvider>
         <App />
       </AuthProvider>
     </GoogleOAuthProvider>
    </BrowserRouter>
  </StrictMode>
)
