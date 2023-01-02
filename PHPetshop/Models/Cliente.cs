namespace PHPetshop.Models {
    public class Cliente : Usuario {
        public string Cpf { get; set; }
        public bool EmailConfirmado { get; set; }
    }
}
