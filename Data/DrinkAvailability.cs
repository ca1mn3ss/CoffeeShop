using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class DrinkAvailability
{
    public int Id { get; set; }

    public int IdcoffeeShop { get; set; }

    public int Iddrinks { get; set; }

    public virtual Shop IdcoffeeShopNavigation { get; set; } = null!;

    public virtual Drink IddrinksNavigation { get; set; } = null!;
}
