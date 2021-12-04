using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Models;

namespace WeatherApp.Adapters
{
    public class ForecastAdapter : BaseAdapter<List>
    {
        List<List> _items;
        Activity _context;

        public ForecastAdapter(Activity context, List<List> items)
        {
            _items = items;
            _context = context;
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
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.forecast_row_layout, null);
            view.FindViewById<TextView>(Resource.Id.dateTimeView).Text = _items[position].dt_txt;
            view.FindViewById<TextView>(Resource.Id.forecastTemperatureView).Text = _items[position].main.temp.ToString()+" °C";
            view.FindViewById<TextView>(Resource.Id.forecastWindView).Text = _items[position].wind.speed.ToString()+" m/s";
            return view;
        }
    }
}