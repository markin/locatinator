# DataSplice .Net GPS Service

Not all devices access the hardware GPS Sensor when using the [Geolocation API](http://dev.w3.org/geo/api/spec-source.html) in the browser. This service consists of:

- natively accessing the hardware GPS sensor
- exposing the GPS data over a locally running WCF web service
- wrapping the web service in a windows service

### Installing the Windows Service

Using the .Net 4.5 installutil: `installUtil DataSpliceGPSService/gpsservice.exe`

> Download the [Offline Installer for .Net 4.5](http://www.microsoft.com/en-us/download/details.aspx?id=40779) and install it onto your device
> The .Net 4.5 installUtil will be located at `C:\Windows\Microsoft.NET\Framework\v4.0.30319\installUtil.exe` 

### Accessing the GPS data

Once your windows service is running you can access the GPS data at: `http://localhost:8000/gps/location`. If all's well you should see something like:

```json
{
  "coords": {
    "latitude": 32.3454567567,
    "longitude": -110.234345356456,
    "accuracy": 20.2
  }
}
```

### Ensure The Service Starts on Boot

You can do this as a script using the [Sc](http://technet.microsoft.com/en-us/library/cc990290.aspx) util. Or you can do it manually through the Windows Services panel:
