# SkyWatch
A simple weather web api that takes in latitude and longitude to find weather data from this hour onwards. 

## How does it work?
It uses YR Api to receive the weather data and parses it to a more compact model. The HTTP Request is defined within '/api/Weather/{lat]&{lon}'. This will in return send back a Json model as a timeseries.
There is also a web application with a simple form that takes in the same values (lat & lon) and gives out a table of the different upcoming weather datas.
