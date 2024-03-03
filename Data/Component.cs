using System;
using System.Collections.Generic;

namespace CoffeeShop1.Data;

public partial class Component
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public decimal Price { get; set; }

    public int Idstorage { get; set; }

    public virtual ICollection<ComponentDrink> ComponentDrinks { get; set; } = new List<ComponentDrink>();

    public virtual Storage IdstorageNavigation { get; set; } = null!;
}
