using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using WeatherApp.Adapters;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var cityEditText = FindViewById<EditText>(Resource.Id.cityEditText);
            var searchButton = FindViewById<Button>(Resource.Id.searchButton);
            var temperatureTextView = FindViewById<TextView>(Resource.Id.temperatureTextView);
            var windTextView = FindViewById<TextView>(Resource.Id.windTextView);
            var weatherImageView = FindViewById<ImageView>(Resource.Id.weatherImageView);

            var weatherService = new WeatherService();

            var listView = FindViewById<ListView>(Resource.Id.forecastListView);

            searchButton.Click += async delegate
            {
                var data = await weatherService.GetCityWeather(cityEditText.Text);
                temperatureTextView.Text = data.main.temp.ToString()+ " °C";
                windTextView.Text = data.wind.speed.ToString()+" m/s";

                var imageBytes = await weatherService.GetImageFromUrl($"https://openweathermap.org/img/wn/{data.weather[0].icon}@2x.png");
                var bitmap = await BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length);
                weatherImageView.SetImageBitmap(bitmap);


                var forecast = await weatherService.GetCityForecast(cityEditText.Text);
                List<List> details = forecast.list;

                var forecastAdapter = new ForecastAdapter(this, details);
                listView.Adapter = forecastAdapter;

            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}