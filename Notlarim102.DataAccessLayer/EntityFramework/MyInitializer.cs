using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notlarim102.Entity;

namespace Notlarim102.DataAccessLayer.EntityFramework
{
    public class MyInitializer:CreateDatabaseIfNotExists<NotlarimContext>
    {
        protected override void Seed(NotlarimContext context)
        {
            //Adding admin user...
            NotlarimUser admin = new NotlarimUser()
            {
                Name = "Onur",
                Surname = "Agici",
                Email = "9731013@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "onuragici",
                Password = "123456",
                ProfileImageFilename = "User1.png",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "onuragici"
            };
            //Adding standart user

            NotlarimUser standartUser = new NotlarimUser()
            {
                Name = "Selin",
                Surname = "Agici",
                Email = "selin@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "selinagici",
                Password = "123456",
                ProfileImageFilename = "User1.png",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "onuragici"
            };
            context.NotlarimUsers.Add(admin);
            context.NotlarimUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                NotlarimUser user = new NotlarimUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user-{i}",
                    Password = "123",
                    ProfileImageFilename = "User1.png",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now ),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user-{i}"
                };
                context.NotlarimUsers.Add(user);
            }

            context.SaveChanges();

            //User list for using..

            List<NotlarimUser> userList = context.NotlarimUsers.ToList();

            //Adding fake categori..
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "onuragici"
                };
                context.Categories.Add(cat);
                //Adding fake Notes...
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {
                    NotlarimUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username
                    };
                    cat.Notes.Add(note);
                    //Adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++)
                    {
                        NotlarimUser comment_owner= userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username
                        };
                        note.Comments.Add(comment);
                    }
                    //Adding fake likes....
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[m]
                        };
                        note.Likes.Add(liked);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
