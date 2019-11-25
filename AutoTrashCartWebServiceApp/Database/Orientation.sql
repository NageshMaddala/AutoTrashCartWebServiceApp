USE [AutoTrashCartDB]
GO

/****** Object:  Table [dbo].[Orientation]    Script Date: 11/25/2019 4:53:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Orientation](
	[OrientationId] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
	[OrientationType] [char](10) NOT NULL,
	[X] [float] NOT NULL,
	[Y] [float] NOT NULL,
	[Z] [float] NOT NULL,
 CONSTRAINT [PK_Orientation] PRIMARY KEY CLUSTERED 
(
	[OrientationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


