namespace application.checkmarx.Commands.AddOrder
{
    public class AddChefCommand : ICommand
    {
      
        public int ChefId { get; set; }
        public string Name { get; set; }
        public string Validation { get; set; }
    }
}
