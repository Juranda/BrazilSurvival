import { useState } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import "./auth.scss";
import { zodResolver } from "@hookform/resolvers/zod";

interface LoginProps {
  isRegister?: boolean;
}

const authSchema = z.object({
  email: z.string().email(),
  password: z.string().min(8).regex(/^(?=.*[A-Z])(?=.*\d)(?=.*[#@!_])[A-Za-z\d#@!_]{8,}$/),
  repeatPassword: z.string().min(8).regex(/^(?=.*[A-Z])(?=.*\d)(?=.*[#@!_])[A-Za-z\d#@!_]{8,}$/).optional()
}).refine((data) => data.password === data.repeatPassword, {
  message: "Suas senhas não batem",
  path: ["repeatPassword"]
});

type FormInput = z.infer<typeof authSchema>;

export function Auth({ isRegister = false }: LoginProps) {
  const { register, handleSubmit, formState: { errors }, setValue, clearErrors } = useForm<FormInput>({
    criteriaMode: "all",
    resolver: zodResolver(authSchema)
  });
  const [_isRegister, setIsRegister] = useState(isRegister);

  function onSubmit(authProps: FormInput) {
    console.log(authProps);
  }

  function toggleForm() {
    if (_isRegister) {
      setValue("repeatPassword", "");
      clearErrors("repeatPassword");
    }


    setIsRegister(prev => !prev);
  }

  return (
    <div className="login">
      <div className="login-wrapper">
        <h1>Brazil Survival</h1>
        <form onSubmit={handleSubmit(onSubmit)} className="login-form">
          <div>
            {/* <label>Email</label> */}
            <input type="email" placeholder="seuemail@provedor.com.br" id="email-input" {...register("email")} />
            {errors.email && <span>{errors.email.message}</span>}
          </div>
          <div>
            {/* <label>Senha</label> */}
            <input type="password" placeholder="Sua senha" id="password-input" {...register("password")} />
          </div>
          {_isRegister && (
            <div>
              {/* <label>Repetir senha</label> */}
              <input type="password" placeholder="Repita sua senha, igual acima" id="repeat-password-input" {...register("repeatPassword")} />
            </div>
          )}

          <button>{(_isRegister ? "Registrar-se" : "Logar")}</button>
        </form>
        <div className="login-alternate">
          <p>{(_isRegister ? "Já tem uma conta?" : "Não tem uma conta?")}</p>
          <p>{(_isRegister ? "Para logar clique " : "Para cadastrar-se clique ")}<strong className="link" onClick={toggleForm}>aqui</strong>.</p>

        </div>
      </div>
    </div>
  );
}
