using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notlarim102.BusinessLayer.Abstract;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;

namespace Notlarim102.BusinessLayer
{
     public class CategoryManager:ManagerBase<Category>
     {
         private NoteManager nm = new NoteManager();
         private LikedManager lm = new LikedManager();
         private CommentManager cmm = new CommentManager();
         //public override int Delete(Category obj)
         //{
         //    foreach (Note note in obj.Notes.ToList())
         //    {
         //        foreach (Liked like in note.Likes.ToList())
         //        {
         //            lm.Delete(like);
         //        }

         //        foreach (Comment comment in note.Comments.ToList())
         //        {
         //            cmm.Delete(comment);
         //        }

         //        nm.Delete(note);
         //    }
         //    return base.Delete(obj);
         //}

         // public List<Category> GetCategories()
        // {
        //     return rcat.List();
        // }
        //public Category GetCategoriesById(int id)
        // {
        //     return rcat.Find(s=>s.Id==id);
        // }

     }
}
