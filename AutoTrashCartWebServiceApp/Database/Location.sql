USE [AutoTrashCartDB]
GO

/****** Object:  Table [dbo].[Location]    Script Date: 11/25/2019 4:52:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
	[LocationType] [char](10) NOT NULL,
	[Latitude0] [float] NOT NULL,
	[Longitude0] [float] NOT NULL,
	[Latitude1] [float] NULL,
	[Longitude1] [float] NULL,
	[Latitude2] [float] NULL,
	[Longitude2] [float] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


