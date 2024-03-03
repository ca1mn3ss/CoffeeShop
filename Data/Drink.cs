using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class Drink
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public decimal Cost { get; set; }

    public string NameOfShop { get; set; } = null!;

    public int IdcoffeeShop { get; set; }

    public virtual ICollection<ClientDrink> ClientDrinks { get; set; } = new List<ClientDrink>();

    public virtual ICollection<ComponentDrink> ComponentDrinks { get; set; } = new List<ComponentDrink>();

    public virtual ICollection<DrinkAvailability> DrinkAvailabilities { get; set; } = new List<DrinkAvailability>();
}
