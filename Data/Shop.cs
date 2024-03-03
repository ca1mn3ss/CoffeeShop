using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class Shop
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<Baristum> Barista { get; set; } = new List<Baristum>();

    public virtual ICollection<DrinkAvailability> DrinkAvailabilities { get; set; } = new List<DrinkAvailability>();
}
