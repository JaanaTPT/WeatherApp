using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Adapters
{
    public class ForecastAdapter : BaseAdapter<List>
    {
        List<List> _items;
        Activity _context;
        CurrentWeatherInfo _currentWeatherInfo;

        public ForecastAdapter(Activity context, List<List> items, CurrentWeatherInfo currentWeatherInfo)
        {
            _items = items;
            _context = context;
            _currentWeatherInfo = currentWeatherInfo;
        }

        public override List this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(_items[position].dt + _currentWeatherInfo.timezone);

            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.forecast_row_layout, null);
            view.FindViewById<TextView>(Resource.Id.dateTimeView).Text = dateTime.ToString("ddd, dd MMM yyy HH:mm");
            view.FindViewById<TextView>(Resource.Id.forecastTemperatureView).Text = Math.Round(_items[position].main.temp).ToString()+" °C";
            view.FindViewById<TextView>(Resource.Id.forecastWindView).Text = Math.Round(_items[position].wind.speed).ToString()+" m/s";
            view.FindViewById<TextView>(Resource.Id.forecastDescriptionView) .Text = _items[position].weather[0].main.ToString().ToUpper();
            return view;
        }
    }
}