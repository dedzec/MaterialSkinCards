using MaterialSkin;
using MaterialSkin.Controls;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System;
using System.Security.Policy;
using System.Reflection.Metadata;
using System.Net.Http.Headers;
using System.Configuration;

namespace MaterialSkinCards
{
    public partial class Form1 : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        static readonly HttpClient HttpClient = new HttpClient();
        static string url = "https://rickandmortyapi.com/api/character/1,2,3,4";

        public Form1()
        {
            InitializeComponent();

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            LoadCards();
        }

        public async void LoadCards()
        {
            const int listNumber = 4;
            MaterialCard[] cards = new MaterialCard[listNumber];
            PictureBox[] pictureBoxes = new PictureBox[listNumber];
            MaterialLabel[] labelsName = new MaterialLabel[listNumber];
            MaterialLabel[] labelsStatus = new MaterialLabel[listNumber];

            List<string> images_list = new List<string>();
            List<string> names_list = new List<string>();
            List<string> status_list = new List<string>();

            for (int i = 0; i < listNumber; i++)
            {
                cards[i] = new MaterialCard();
                pictureBoxes[i] = new PictureBox();
                labelsName[i] = new MaterialLabel();
                labelsStatus[i] = new MaterialLabel();

                cards[i].Width = 210;
                cards[i].Height = 300;

                pictureBoxes[i].Width = 210;
                pictureBoxes[i].Height = 150;
                pictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;

                labelsName[i].Location = new Point(17, 168);
                labelsName[i].AutoSize = true;

                labelsStatus[i].Location = new Point(17, 205);

                flowLayoutPanel1.Controls.Add(cards[i]);
                cards[i].Controls.Add(pictureBoxes[i]);
                cards[i].Controls.Add(labelsName[i]);
                cards[i].Controls.Add(labelsStatus[i]);

                //var url_json = new WebClient().DownloadString(url);
                //var data = JsonConvert.DeserializeObject<List<Character>>(url_json);

                HttpResponseMessage response = await HttpClient.GetAsync(url);
                //var content = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(content);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Character>>(jsonResponse);

                foreach (var item in data)
                {
                    Console.WriteLine($"{item}\n");
                    images_list.Add(item.Image);
                    names_list.Add(item.Name);
                    status_list.Add(item.Status);

                    string[] imagesArray = images_list.ToArray();
                    string[] namesArray = names_list.ToArray();
                    string[] statusArray = status_list.ToArray();

                    pictureBoxes[i].ImageLocation = imagesArray[i];
                    labelsName[i].Text = namesArray[i];
                    labelsStatus[i].Text = statusArray[i];
                }
            }
        }

        /*static async Task<string> GetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await HttpClient.GetAsync(url);
                //var content = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(content);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");
                return jsonResponse;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception}\n");
            }
        }*/
    }
}