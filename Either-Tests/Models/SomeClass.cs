namespace Either_Tests.Models
{
    public class SomeClass
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name}";
        }
    }
}
