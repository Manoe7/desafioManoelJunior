using System;
using System.Collections.Generic;
using App1.Models;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
        public List<History> StatusHistoryContent { get; set; }
    }
}
