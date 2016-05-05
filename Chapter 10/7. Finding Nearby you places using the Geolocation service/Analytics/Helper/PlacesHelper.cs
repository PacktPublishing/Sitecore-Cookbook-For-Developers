using Sitecore;
using Sitecore.Analytics;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Analytics.Helper
{
    public class PlacesHelper
    {
        public static List<Item> GetNearbyPlaces()
        {
            // For Reference
            //double latitude = 40.69;
            //double longitude = -73.95;

            var geoData = Tracker.Current.Interaction.GeoData;
            if (geoData.Latitude == null)
                return null;

            double latitude = (double)geoData.Latitude;
            double longitude = (double)geoData.Longitude;

            var orderedDistances = FindNearbyPlaces(latitude, longitude);
            var places = new List<Item>();
            foreach (var iterator in orderedDistances)
            {
                places.Add(iterator.Key);
            }
            return places;
        }

        private static IEnumerable<KeyValuePair<Item, double>> FindNearbyPlaces(double latitude, double longitude)
        {
            Item locations = Context.Database.GetItem("/sitecore/content/Home/Service Stations/");
            var places = new Dictionary<Item, double>();
            foreach (Item place in locations.Children)
            {
                double placeLat = double.Parse(place["Latitude"]);
                double placeLong = double.Parse(place["Longitude"]);

                double distance = FindDistance(placeLat, placeLong, latitude, longitude);
                places.Add(place, distance);
            }

            return places.OrderBy(p => p.Value).Take(5);
        }

        private static double FindDistance(double lat1, double long1, double lat2, double long2)
        {
            double R = 6371;
            double dLat = (lat2 - lat1) * Math.PI / 180;
            double dLon = (long2 - long1) * Math.PI / 180;
            lat1 *= Math.PI / 180;
            lat2 *= Math.PI / 180;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
        }
    }
}
