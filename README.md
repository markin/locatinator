# DataSplice .Net GPS Service

Not all devices access the hardware GPS Sensor when using the [Geolocation API](http://dev.w3.org/geo/api/spec-source.html) in the browser. This service consists of:

- natively accessing the hardware GPS sensor
- exposing the GPS data over a locally running WCF web service
- wrapping the web service in a windows service

### Installing the Windows Service

Using the .Net 4.5 installutil: `installutil DataSpliceGPSService/gpsservice.exe`

### Accessing the GPS data

Once your windows service is running you can access the GPS data at: `http://localhost:8000/gps/location`

### Example GPS Data
