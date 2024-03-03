using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class Baristum
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int Seniority { get; set; }

    public int Idshop { get; set; }

    public virtual Shop? IdshopNavigation { get; set; } 
}
