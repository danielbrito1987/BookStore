namespace BookStore.Api.Commands
{
    public class CreateLivroCommand
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }

        public List<int> AutoresIds { get; set; }
        public List<int> AssuntosIds { get; set; }
        public List<CreatePrecoCommand> Precos { get; set; }
    }
}
