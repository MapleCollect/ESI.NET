﻿using Newtonsoft.Json;
using System;

namespace ESI.NET.Models.Industry
{
    public class Entry
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("solar_system_id")]
        public int SolarSystemId { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }
    }
}