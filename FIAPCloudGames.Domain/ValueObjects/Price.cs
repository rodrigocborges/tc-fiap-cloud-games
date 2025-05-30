﻿using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FIAPCloudGames.Domain.ValueObjects
{
    [Owned]
    public class Price
    {
        public decimal Value { get; private set; }

        private Price() { }

        public Price(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.");
            Value = decimal.Round(value, 2);
        }

        public override bool Equals(object? obj) => obj is Price other && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString("C2", CultureInfo.CurrentCulture);
    }
}
