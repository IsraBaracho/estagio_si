import { useState } from "react";

export default function Register() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState("User");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError("");
    setSuccess("");
    try {
      const response = await fetch("http://localhost:5000/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, email, password, role }),
      });
      const data = await response.json();
      if (!response.ok) throw new Error(data.error || "Erro ao cadastrar");
      setSuccess("Usuário cadastrado com sucesso! Faça login.");
      setName(""); setEmail(""); setPassword("");
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-green-100 to-blue-100">
      <form
        onSubmit={handleSubmit}
        className="bg-white p-8 rounded-lg shadow-md w-full max-w-sm flex flex-col gap-4"
      >
        <h2 className="text-2xl font-bold text-green-700 mb-2 text-center">Cadastro</h2>
        {error && (
          <div className="bg-red-100 text-red-700 px-3 py-2 rounded text-sm">{error}</div>
        )}
        {success && (
          <div className="bg-green-100 text-green-700 px-3 py-2 rounded text-sm">{success}</div>
        )}
        <div>
          <label className="block text-sm font-medium text-gray-700">Nome</label>
          <input
            type="text"
            className="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-green-500 focus:border-green-500"
            value={name}
            onChange={e => setName(e.target.value)}
            required
            autoFocus
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">E-mail</label>
          <input
            type="email"
            className="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-green-500 focus:border-green-500"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Senha</label>
          <input
            type="password"
            className="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-green-500 focus:border-green-500"
            value={password}
            onChange={e => setPassword(e.target.value)}
            required
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Tipo de Usuário</label>
          <select
            className="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-green-500 focus:border-green-500"
            value={role}
            onChange={e => setRole(e.target.value)}
            required
          >
            <option value="User">Usuário</option>
            <option value="Attendant">Atendente</option>
            <option value="Coordinator">Coordenador</option>
          </select>
        </div>
        <button
          type="submit"
          disabled={loading}
          className="bg-green-600 hover:bg-green-700 text-white font-bold py-2 rounded focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-offset-2 transition disabled:opacity-60"
        >
          {loading ? "Cadastrando..." : "Cadastrar"}
        </button>
        <a
          href="/"
          className="text-center text-sm text-blue-600 hover:underline mt-2"
        >Já tem conta? Faça login</a>
      </form>
    </div>
  );
}
