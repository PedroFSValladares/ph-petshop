namespace PHPetshop.Models {
    public class Pet {
        public int Id { get; set; }
        public string Nome { get; set;}
        public DateOnly DataResgate { get; set; }
        public int Idade { get; set; }
        public char Sexo { get; set; }
        public string Raca { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public PetPorte Porte { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set;}
        public Imagem Imagem { get; set; }
    }

    public class Imagem {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public byte[] Dados { get; set; }
    }
}
