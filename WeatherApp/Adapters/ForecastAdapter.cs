﻿using Android.App;
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
    public class ForecastAdapter : BaseAdapter<WeatherInfo>
    {
        List<WeatherInfo> _items;
        Activity _context;

        public ForecastAdapter(Activity context, List<WeatherInfo> items)
        {
            _items = items;
            _context = context;
        }

        public override WeatherInfo this[int position]
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
            view.FindViewById<TextView>(Resource.Id.dateTimeView).Text = _items[position].list.dt_txt;
            view.FindViewById<TextView>(Resource.Id.forecastTemperatureView).Text = _items[position].list.main.temp.ToString();
            view.FindViewById<TextView>(Resource.Id.forecastWindView).Text = _items[position].list.wind.ToString();
            return view;
        }
    }
}