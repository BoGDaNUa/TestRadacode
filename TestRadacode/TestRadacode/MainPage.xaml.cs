using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

namespace TestRadacode
{
    
    public partial class MainPage : ContentPage
    {
        internal Blank NewBlank=new Blank();
        Dictionary<string, string> Countres;
        Dictionary<string, string> City;
        Dictionary<string, string> University;
        Dictionary<string, Dictionary<string, List<string>>> nameToColor = new Dictionary<string, Dictionary<string, List<string>>>
        {
            { "Ukrainian", new Dictionary<string,List<string>> {{"Kiev",new List<string>{ { "Kpi"}, {"Sheva" } }}, { "Lviv", new List<string> { { "Lpi" }, { "ShevaL" } } } } },
            { "Poland", new Dictionary<string,List<string>> {{"Krakov",new List<string>{ { "K1"}, {"Shr" } }}, { "Varshava", new List<string> { { "Var1" }, { "ShevaVVV" } } } } }

        };
        public MainPage()
        {
            InitializeComponent();
            foreach (string colorName in nameToColor.Keys)
            {
                PickerCountry.Items.Add(colorName);
            }
            LoadCountry();
            NameUser.Completed += EnterName;
            FamallyUser.Completed += EnterFemali;
            //PickerCity.TextChanged += EnterCity;
            ListFindCity.ItemSelected += CitySelected;
            PickerUniversity.TextChanged += EnterUniversity;
            ListUniversity.ItemSelected += UniversitySelected;
            EndCreate.Clicked += End;
            PickerCountry.SelectedIndexChanged += (sender, args) =>
            {
                if (PickerCountry.SelectedIndex == -1)
                {
                    PickerCity.IsVisible = false;
                }
                else
                {
                    NewBlank.CountryID = Countres[(string)(PickerCountry.Items[PickerCountry.SelectedIndex])];
                    NewBlank.Country = (string)(PickerCountry.Items[PickerCountry.SelectedIndex]);
                    PickerCity.IsVisible = true;
                    ListFindCity.IsVisible = true;
                }
            };
            PickerCity.TextChanged += (sender, args) =>
            {
                string str = PickerCity.Text;
                LoadCity(str);
            };
        }

        private void EnterName(object sender, EventArgs e)
        {
            NewBlank.Name = NameUser.Text;
            FamallyUser.IsVisible = true;
            FamallyUser.Focus();
        }

        private void EnterFemali(object sender, EventArgs e)
        {
            NewBlank.Famaly = FamallyUser.Text;
            LabelCountry.IsVisible = true;
            PickerCountry.IsVisible = true;
        }

        private void CitySelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            PickerCity.Text = (string)ListFindCity.SelectedItem;
            NewBlank.City=(string)ListFindCity.SelectedItem;
            NewBlank.CityId =City[NewBlank.City];
            ListFindCity.IsVisible = false;
            PickerUniversity.IsVisible = true;
            ListUniversity.IsVisible = true;
        }


        private void End(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ResultatPage(this));
        }

        private async void EnterUniversity(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string str = PickerUniversity.Text;
            try
            {
                University = await VKAPi.University( str);
                List<string> Ls = new List<string>();

                foreach (string colorName in University.Keys)
                {
                    Ls.Add(colorName);
                }
                ListUniversity.ItemsSource = Ls;

            }
            catch
            {
            }
        }
        private void UniversitySelected(object sender, SelectedItemChangedEventArgs e)
        {
            NewBlank.University=(string)ListUniversity.SelectedItem;
            NewBlank.UniId = University[NewBlank.University];
            PickerUniversity.Text = (string)ListUniversity.SelectedItem;
            ListUniversity.IsVisible = false;
            EndCreate.IsVisible = true;
        }

        private void EnterCity(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        async void LoadCountry()
        {
            Countres = await VKAPi.Countries();
            PickerCountry.Items.Clear();

            foreach (string colorName in Countres.Keys)
            {
                PickerCountry.Items.Add(colorName);
            }
        }
        async void LoadCity(string str)
        {
            try
            {
                City = await VKAPi.City(Countres[(string)(PickerCountry.Items[PickerCountry.SelectedIndex])], str);
                List<string> Ls = new List<string>();

                foreach (string colorName in City.Keys)
                {
                    Ls.Add(colorName);
                }
                ListFindCity.ItemsSource = Ls;

            }
            catch
            {
            }
        }
    }
}
