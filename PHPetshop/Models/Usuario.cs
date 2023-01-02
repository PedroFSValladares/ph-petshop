namespace PHPetshop.Models {
    public class Usuario {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Nome { get; set; }
        public Cargo Cargo { get; set; }
        public Endereco Endereco { get; set; }
    }
    public enum Cargo {
        User,
        Admin
    }
}
