using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class ClientDrink
{
    public int Id { get; set; }

    public int Idclient { get; set; }

    public int Iddrink { get; set; }

    public virtual Client? IdclientNavigation { get; set; } 

    public virtual Drink? IddrinkNavigation { get; set; } 
}
