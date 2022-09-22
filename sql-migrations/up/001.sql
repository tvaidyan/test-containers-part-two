SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeatherForecasts](
	[WeatherForecastId] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](1000) NOT NULL,
	[TemperatureC] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Summary] [nvarchar](1000) NOT NULL
 CONSTRAINT [PK_WeatherForecast] PRIMARY KEY CLUSTERED 
(
	[WeatherForecastId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO