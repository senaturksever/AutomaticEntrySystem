# AutomaticEntrySystem

Kayıt olmak için
https://localhost:7096/api/Mobile/register 
POST 
request parametreleri: string Username,string Email, string Password

Login işlemi için
https://localhost:7096/api/Mobile/login
POST
request parametresi: string Email, string Password

Mobil anasayfa için
https://localhost:7096/api/Mobile/mobilPage
POST
request parametresi: bool isRouteWebPage

Web Anasafası için
https://localhost:7096/api/Web/webHomePage
GET

UnitTest:
 UnsuccessfulTest
   Duration: 319 ms
  Message: 
      Expected: False
      But was:  True

 SuccessfulTest
   Duration: 913 ms

 UnsuccessfulTest
   Source: RegisterTests.cs line 74
   Duration: 221 ms
  Message: 
  Expected: False
  But was:  True
  Stack Trace: 
RegisterTests.UnsuccessfulTest() line 98

 UnsuccessfulTest
   Source: RegisterTests.cs line 74
   Duration: 8,2 sec

 SuccessfulTest
   Source: RegisterTests.cs line 19
   Duration: 913 ms

Veritabanı için : 
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
