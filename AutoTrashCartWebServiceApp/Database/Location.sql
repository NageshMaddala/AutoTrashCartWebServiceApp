/****** Object:  Table [dbo].[Location]    Script Date: 2/20/2020 2:47:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [nvarchar](50) NOT NULL,
	[LocationType] [char](10) NOT NULL,
	[Latitude0] [float] NOT NULL,
	[Longitude0] [float] NOT NULL,
	[Latitude1] [float] NULL,
	[Longitude1] [float] NULL,
	[Latitude2] [float] NULL,
	[Longitude2] [float] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


