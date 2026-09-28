namespace Core.Entities
{
    public class Location
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;
        public string StreetNumber { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;

        public void SetId(Guid id)
        {
            if (id == Guid.Empty) throw new ValidationException("Location ID is required");
            Id = id;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ValidationException("Location name is required");
            Name = name;
        }

        public void ChangeAddress(string street, string streetNumber, string postalCode, string city)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ValidationException("Street is required");
            if (string.IsNullOrWhiteSpace(streetNumber)) throw new ValidationException("Street number is required");
            if (string.IsNullOrWhiteSpace(postalCode)) throw new ValidationException("Postal code is required");
            if (string.IsNullOrWhiteSpace(city)) throw new ValidationException("City numver is required");

            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
        }
    }
}