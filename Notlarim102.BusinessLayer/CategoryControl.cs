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
    public class CategoryControl
    {
        private NotlarimContext db = new NotlarimContext();

        public void Insert(Category obj)
        {
            db.Categories.Add(obj);
            db.SaveChanges();
        }
    }

    public class NoteControl
    {
        private NotlarimContext db = new NotlarimContext();

        private Repo<Note> nrepo = new Repo<Note>();


        public void Insert(Note obj)
        {
            Note note = new Note()
            {
                Title = "Deneme",
                Text = "Deneme yazisi"
            };
            nrepo.Insert(note);
            //db.Notes.Add(obj);
            //db.SaveChanges();
        }
    }

    public class Repo<T> where T:class
    {
        private NotlarimContext db = new NotlarimContext();
        public void Insert(T obj)
        {
            db.Set<T>().Add(obj);
            db.SaveChanges();
        }
    }
}
