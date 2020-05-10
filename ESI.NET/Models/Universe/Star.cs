﻿using Newtonsoft.Json;

namespace ESI.NET.Models.Universe
{
    public class Star
    {
        [JsonProperty("age")]
        public long Age { get; set; }

        [JsonProperty("luminosity")]
        public decimal Luminosity { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("radius")]
        public long Radius { get; set; }

        [JsonProperty("solar_system_id")]
        public int SolarSystemId { get; set; }

        [JsonProperty("spectral_class")]
        public string SpectralClass { get; set; }

        [JsonProperty("temperature")]
        public int Temperature { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }
    }
}