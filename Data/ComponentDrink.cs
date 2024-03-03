using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class ComponentDrink
{
    public int Id { get; set; }

    public int Idcomponent { get; set; }

    public int Iddrink { get; set; }

    public virtual Component IdcomponentNavigation { get; set; } = null!;

    public virtual Drink IddrinkNavigation { get; set; } = null!;
}
