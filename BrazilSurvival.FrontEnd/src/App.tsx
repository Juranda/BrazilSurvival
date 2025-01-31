import { FormEvent, FormEventHandler, useRef } from "react";

export default function App() {

  return <Login />
}

interface LoginProps {
  isRegister?: boolean
}

interface LoginPostRequest {
  email: string,
  password: string,
  repeatPassword: string
}

interface LoginResponse {
  token: string
}

function Login({ isRegister }: LoginProps) {
  const passwordRef = useRef<HTMLInputElement>(null);
  const emailRef = useRef<HTMLInputElement>(null);
  const repeatPasswordRef = useRef<HTMLInputElement>(null);

  function onSubmit(event: FormEvent) {
    event.preventDefault();

    const postObj: LoginPostRequest = {
      "email": emailRef.current?.value ?? "",
      "password": passwordRef.current?.value ?? "",
      "repeatPassword": repeatPasswordRef.current?.value ?? "",
    };

    console.log(postObj);
    

    //postLoginRegister(postObj);
  }

  async function postLoginRegister(data: LoginPostRequest) {
    const response = await fetch(`http://localhost:${import.meta.env.SERVER_PORT}/login`, {
      headers: {
        "Content-Type": "application/json",
      },
      method: "POST",
      body: JSON.stringify(data)
    });

    const json = JSON.parse(await response.json()) as LoginResponse;
    const token = json.token;

    console.log(token);
  }

  return (
    <div className="login">
      <div className="login-wrapper">
        <h1>Brazil Survival</h1>
        <form onSubmit={onSubmit} className="login-form">
          <input ref={emailRef} type="email" name="email" id="email-input" />
          <input ref={passwordRef} type="password" name="password" id="password-input" />
          {
            isRegister && <input ref={repeatPasswordRef} type="repeat-password" name="repeat-password" id="password-input" />
          }
          <button  >{(isRegister ? "Registrar-se" : "Logar")}</button>
        </form>
        <div className="">
          {(isRegister ? "Já tem uma conta? Para logar clique " : "Não tem uma conta? Para cadastrar-se clique ")} <a href="https://www.google.com.br">aqui</a>.
        </div>
      </div>
    </div>
  );
}