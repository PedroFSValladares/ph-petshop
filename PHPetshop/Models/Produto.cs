namespace PHPetshop.Models {
    public class Produto {
        public string Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string Fabricante { get; set; }
        public string Categoria { get; set; }
        public Tipo TipoProduto { get; set; }
        public byte[] Imagem { get; set; }
    }
}
