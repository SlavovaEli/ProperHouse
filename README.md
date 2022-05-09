# ProperHouse
ASP.NET Core App for the final project at SoftUni.
**ProperHouse** is a web app for renting and listing properties.

## How does it work
- Guests
   - Can view the list of properties without seeing any details
   - Can search properties without seeing any details
   - Can register or login
- Logged users
   - Can view all properties and their details
   - Can add properties to favorites and view their Favorites
   - Can make reservations and view their Reservations
   - Can become Owner and list properties for renting
- Owners
   - Can list properties for renting but they become visible after admin's approval
   - Can edit their properties but they become visible after admin's approval
   - Can delete their properties
- Admin
   - Has access to the admin area
   - Only after his approval a property is visible
   - Can edit property
   - Can delete property

## Test Accounts 
- Admin
   - Email: admin@prop.com
   - Password: admin12
- User
   - Email: third@user.com
   - Password: third123
- Owner
   - Email: eli@eli.com
   - Password: 123456
 
 # Used Technologies:
 - ASP.NET Core
 - Entity Framework Core
 - Bootstrap
 - xUnit
 - Moq
