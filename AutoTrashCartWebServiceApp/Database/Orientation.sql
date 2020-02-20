/****** Object:  Table [dbo].[Orientation]    Script Date: 2/20/2020 2:47:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Orientation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrientationId] [nvarchar](50) NOT NULL,
	[OrientationType] [char](10) NOT NULL,
	[X] [float] NOT NULL,
	[Y] [float] NOT NULL,
	[Z] [float] NOT NULL,
 CONSTRAINT [PK_Orientation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


