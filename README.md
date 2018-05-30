SquirrelFramework
=================
Squirrel Framework - A lightweight back-end framework and utilities kit based on MongoDB/Redis.

Get package
------------
You can get the latest stable release from the [official Nuget.org feed](https://www.nuget.org/packages/SquirrelFramework)

https://www.nuget.org/packages/SquirrelFramework

## Getting Started
1. Create a .NET project, please ensure that the target framework `MUST be .NET Framework 4.6.2 or later`

2. Get the Nuget package by searcing the keyword "SquirrelFramework" or using the Project Manager
```Shell
    Install-Package SquirrelFramework -Version 1.0.13
```

3. Create your Domain Model
```C#
    using SquirrelFramework.Domain.Model;

    [Database("YourDatabaseName")]
    [Collection("UsersCollectionName")]
    public class User : DomainModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
```
`The [Database] attribute is not necessary`, you can set the default MongoDB database name at `/Config/mongodb.config`

4. Create your Repository for MongoDB CRUD
```C#
    using SquirrelFramework.Repository;

    public class UserRepository: RepositoryBase<User> {}
```

5. Now you are free to perform various operations on MongoDB, here are some examples
```C#
            var userRepo = new UserRepository();
```

* Add a new user record
```C#
            userRepo.Add(new User{
                Name = "Hendry",
                Gender = "Male",
                Age = "18",
                Geolocation = new Geolocation(121.551949, 38.890957) 
            });
```

* Get all users who are 2 kilometers away from me
```C#
            userRepo.GetNearBy(new Geolocation(121.551949, 38.890957), 2000);
```

* Bulk delete users who are older than 25 asynchronously
```C#
            userRepo.DeleteManyAsync(u => u.Age > 25);
```

* Get the third page (15 records) of all user data and descending sort by the Age field 
```C#
            //  Method signature 
            //  public IEnumerable<TDomain> GetAllByPageSortBy(int pageIndex, int pageSize, Expression<Func<TDomain, object>> sortBy, bool isSortByDescending = false);
            userRepo.GetAllByPageSortBy(2, 15, u=>u.Age, true);
```

6. If your data collection is dynamic, for example you have multiple collections to store your order information:
* Users201801
* Users201802
* Users201803
* ...

You can use the CustomizedRepositoryBase class as a base class
```C#
    public class UserRepository: CustomizedRepositoryBase<User> {}
```
Then you can provide the specific collection name for each CRUD operation.

* Add a new user record
```C#
            var userRepo = new UserRepository();
            userRepo.Add("Users201805", new User{
                Name = "Hendry",
                Gender = "Male",
                Age = "18",
                Geolocation = new Geolocation(121.551949, 38.890957) 
            });
```

7. Create your Domain Service (This is not a necessary step)
```C#
    public class UserService : ServiceBase<User, UserRepository> {}
```

Contributing
------------

* Hendry Qi              me@nap7.com
`If you have any questions, please feel free to contact me`
