using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Fullname => Name + " " + Surname;

    public virtual ICollection<ClientDrink> ClientDrinks { get; set; } = new List<ClientDrink>();
}
