  INSERT INTO WeatherForecasts ([City]
      ,[TemperatureC]
      ,[CreatedDate]
      ,[Summary])
	  VALUES(@city, @temperatureC, GETUTCDATE(), @summary);