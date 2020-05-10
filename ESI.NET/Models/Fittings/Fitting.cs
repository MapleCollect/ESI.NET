﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESI.NET.Models.Fittings
{
    public class Fitting
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fitting_id")]
        public int FittingId { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; } = new List<Item>();

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ship_type_id")]
        public int ShipTypeId { get; set; }
    }

    public class Item
    {
        [JsonProperty("flag")]
        public int Flag { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }
    }
}