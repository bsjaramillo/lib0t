﻿using Newtonsoft.Json;

public class Permissions
{
    [JsonProperty("admin")]
    public bool Admin { get; set; }

    [JsonProperty("push")]
    public bool Push { get; set; }

    [JsonProperty("pull")]
    public bool Pull { get; set; }
}