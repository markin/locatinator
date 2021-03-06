﻿using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Newtonsoft.Json;
using System.ServiceModel.Web;

namespace DataSplice.Services.GPS
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        AddressFilterMode = AddressFilterMode.Any
    )]
    public class GPSWebService : IGPSWebService
    {
        private GeoCoordinateWatcher watcher = null;
        private GeoPosition<GeoCoordinate> lastKnownPosition = null;

        public string Location()
        {
            // Support CORS 
            // See: http://dhvik.blogspot.co.uk/2011/06/supporting-cross-origin-resource.html
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            // Identify preflight request and add extra headers  
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                WebOperationContext.Current.OutgoingResponse.Headers
                    .Add("Access-Control-Allow-Methods", "POST, OPTIONS, GET");
                WebOperationContext.Current.OutgoingResponse.Headers
                    .Add("Access-Control-Allow-Headers",
                         "Content-Type, Accept, Authorization, x-requested-with, x-ds-sessionname");
                return null;
            }

            StartWatcher();
            var payloadString = ToJson(ToGeoLocation(lastKnownPosition));
            if (lastKnownPosition != null)
            {
                var payload = ToGeoLocation(lastKnownPosition);
                payloadString = ToJson(payload);
                System.Diagnostics.Trace.WriteLine("Known Position: " + payloadString);
            }
            return payloadString;
        }

        private string ToJson(object payload)
        {
            return JsonConvert.SerializeObject(payload);
        }

        private object UnknownGeoLocation()
        {
            return new
            {
                code = 2,
                message = "Position unavailable"
            };
        }

        private object ToGeoLocation(GeoPosition<GeoCoordinate> position) 
        {
            if (position == null || position.Location.IsUnknown)
            {
                return UnknownGeoLocation();
            }
            var location = position.Location;
            return new
            {
                coords = new
                {
                    latitude = location.Latitude,
                    longitude = location.Longitude,
                    accuracy = location.HorizontalAccuracy
                }
            };
        }

        private void StartWatcher()
        {
            if (this.watcher == null)
            {
                System.Diagnostics.Trace.WriteLine("Creating GPS watcher");
                this.watcher = new GeoCoordinateWatcher();
            }
            watcher.PositionChanged += PositionChanged;
            watcher.StatusChanged += StatusChanged;
            watcher.Start();
        }

        private void StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Status changed");
            this.lastKnownPosition = null;
        }

        private void PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            System.Diagnostics.Trace.WriteLine("Position changed");
            this.lastKnownPosition = e.Position;
        }
    }
}
