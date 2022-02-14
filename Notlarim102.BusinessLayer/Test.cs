using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notlarim102.DataAccessLayer;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;

namespace Notlarim102.BusinessLayer
{
    public class Test
    {
        private Repository<NotlarimUser> ruser = new Repository<NotlarimUser>();
        private Repository<Category> rcat = new Repository<Category>();
        private Repository<Note> rnote = new Repository<Note>();
        private Repository<Comment> rcom = new Repository<Comment>();
        private Repository<Liked> rlike = new Repository<Liked>();

        public Test()
        {
            //NotlarimContext db = new NotlarimContext();
            //db.Categories.ToList();
            //db.Database.CreateIfNotExists();

            var test = rcat.List();
            var test1 = rcat.List(x=>x.Id>5);

        }

        public void InsertTest()
        {
            int result = ruser.Insert(new NotlarimUser()
            {
                Name = "Abuzittin",
                Surname = "Tarhanaci",
                Email = "bakisine@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "abutar",
                Password = "123",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "abutar"
            });
        }

        public void UpdateTest()
        {
            NotlarimUser user = ruser.Find(x => x.Username == "abutar");

            if (user!=null)
            {
                user.Password = "1111111";
                ruser.Update(user);
                
            }
        }

        public void DeleteTest()
        {
            NotlarimUser user = ruser.Find(x => x.Username == "abutar");
            if (user != null)
            {
                ruser.Delete(user);
            }
        }

        public void CommentTest()
        {
            NotlarimUser user = ruser.Find(s => s.Id == 1);
            Note note = rnote.Find(s => s.Id == 3);
            

            Comment comment = new Comment()
            {
                Text = "Bu bir test datasidir.",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "onuragici",
                Note = note,
                Owner = user,
            };
            rcom.Insert(comment);
        }
    }
}
