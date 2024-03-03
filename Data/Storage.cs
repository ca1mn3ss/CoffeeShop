using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class Storage
{
    public int Id { get; set; }

    public int AmountOfProducts { get; set; }

    public int Capacity { get; set; }

    public string Provisioner { get; set; } = null!;

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}
