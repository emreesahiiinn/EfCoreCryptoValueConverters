using Domain.Attributes;
using Domain.Entities.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    [Encrypted] public string Email { get; set; }
    [Encrypted] public string PhoneNumber { get; set; }
}