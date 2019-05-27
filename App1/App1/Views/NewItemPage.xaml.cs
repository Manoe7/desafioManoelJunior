using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App1.Models;
using System.Linq;
using Newtonsoft.Json;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        private ItemDetailPage EditParent { get; set; }
        public List<string> ItemsSourceList { get; set; } = new List<string>() { "Aberta", "Em Andamento", "Resolvida", "Cancelada" };
        public int ListIndexSelected { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            Title = "Nova Tarefa";
            ListIndexSelected = 0;
            Item = new Item
            {
                Text = "Nome da tarefa",
                Description = "Descrição da tarefa"
            };

            BindingContext = this;
        }

        public NewItemPage(Item item, ItemDetailPage parent)
        {
            InitializeComponent();

            Title = item.Text;
            Item = item;
            EditParent = parent;
            ListIndexSelected = ItemsSourceList.IndexOf(item.Status);

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (EditParent != null)
            {
                if (Item.Status != ItemsSourceList.ElementAt(ListIndexSelected))
                {
                    Item.Status = ItemsSourceList.ElementAt(ListIndexSelected);
                    List<History> StatusHistoryList = JsonConvert.DeserializeObject<List<History>>(Item.StatusHistory);
                    StatusHistoryList.Add(new History { Status = Item.Status, Date = DateTime.Now.ToString() });
                    Item.StatusHistory = JsonConvert.SerializeObject(StatusHistoryList);
                }
                EditParent.EditPageCallback(Item);
            }
            else
            {
                Item.Status = ItemsSourceList.ElementAt(ListIndexSelected);
            }
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}