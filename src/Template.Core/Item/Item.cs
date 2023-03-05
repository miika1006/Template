using System;
using Template.Core.Base;

namespace Template.Core.Item
{
	public class Item : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public virtual List<ItemProperty> Properties { get; set; } = new List<ItemProperty>();
    }
}

