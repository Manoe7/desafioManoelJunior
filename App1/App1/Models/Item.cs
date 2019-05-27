using SQLite;
using System;
using System.Collections.Generic;

namespace App1.Models
{
    public class Item
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public string StatusHistory { get; set; }
        public bool Warning
        {
            get
            {
                return (DateTime.Now > StartDate.AddDays(10) && (Status != "Resolvida" && Status != "Cancelada"));
            }
        }
    }
}