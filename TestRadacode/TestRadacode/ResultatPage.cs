using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TestRadacode
{
    public class ResultatPage : ContentPage
    {
        private MainPage mainPage;
        Label Name      =new Label(),
            Famely      =new Label(),
            Country     =new Label(),
            City        =new Label(), 
            University  =new Label();

        public ResultatPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Дані Бланку",HorizontalOptions=LayoutOptions.Center },Name,Famely,Country,City,University
                }
                
            };
        }

        public ResultatPage(MainPage mainPage):this()
        {
            this.mainPage = mainPage;
            Name.Text = "Ім'я " +mainPage.NewBlank.Name;
            Famely.Text =  "Призвіще "    +mainPage.NewBlank.Famaly;
            City.Text = "Місто " +mainPage.NewBlank.City;
            Country.Text =     "Країна " +mainPage.NewBlank.Country;
            University.Text =     "ВНЗ " +mainPage.NewBlank.University;
        }
    }
}
