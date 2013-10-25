namespace ProtoConsole
{
    public class Controller
    {
        private readonly IRepository<Entity> repository;

        public Controller(
            IRepository<Entity> repository) {
                this.repository = repository;
            }

        public bool Index() {
            this.repository.ReadTs();
            return true;
        }

        public bool Create(Entity e) {
            this.repository.CreateT(e);
            return true;
        }
    }
}