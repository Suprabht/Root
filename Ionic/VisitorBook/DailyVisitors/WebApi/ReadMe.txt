Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.Identity
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.SqlServer

add-migration "Initial"
update-database

http://localhost:12558/api/Authentication/Register
{
	"username":"suprabhatpaul",
	"email":"suprabhatpaul@sdl.com",
	"password":"Welcome@1234"
}

http://localhost:12558/api/Authentication/Register
{
	"username":"normalUser",
	"email":"normalUser@sdl.com",
	"password":"Welcome@1234"
}

http://localhost:12558/api/Authentication/RegisterApprover
{
	"username":"manager",
	"email":"manager@sdl.com",
	"password":"Welcome@1234"
}
http://localhost:12558/api/Authentication/Login
{
	"username":"suprabhatpaul",
	"password":"Welcome@1234"
}