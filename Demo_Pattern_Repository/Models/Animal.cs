﻿namespace Demo_Pattern_Repository.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsDomesticated { get; set; }
        public int? LifeExpectancy { get; set; }

        public Familia Familia { get; set; } = null!;
        public IEnumerable<Region> Regions { get; set; }
    }
}