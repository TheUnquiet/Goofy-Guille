using Domain.Services.Exceptions;

namespace Domain.Models
{
    public class Club
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;

        public void SetId(Guid id)
        {
            if (id == Guid.Empty) throw new ValidationException("Club ID is required.");
            Id = id;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ValidationException("Club name is required.");
            Name = name;
        }
    }
}
