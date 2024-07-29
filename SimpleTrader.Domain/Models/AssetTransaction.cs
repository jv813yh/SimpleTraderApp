﻿namespace SimpleTrader.Domain.Models
{
    public class AssetTransaction
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public bool IsPurchase { get; set; }
        public Stock Stock { get; set; }
        public int SharesAmount { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}
