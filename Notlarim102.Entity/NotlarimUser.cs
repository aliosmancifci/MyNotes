using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblNotlarimUsers")]
    public class NotlarimUser : MyEntityBase
    {
        [DisplayName("Isim"), 
         StringLength(30, ErrorMessage = "{0} max.{1} karakter olmali")]
        public string Name { get; set; }
        [DisplayName("Soyad"), 
         StringLength(30, ErrorMessage = "{0} max.{1} karakter olmali")]
        public string Surname { get; set; }
        [DisplayName("Kullanici Adi"), 
         StringLength(30, ErrorMessage = "{0} max.{1} karakter olmali"), 
         Required(ErrorMessage = "{0} alani gereklidir.")]
        public string Username { get; set; }
        [DisplayName("E-Posta"), 
         StringLength(100, ErrorMessage = "{0} max.{1} karakter olmali"), 
         Required(ErrorMessage = "{0} alani gereklidir.")]
        public string Email { get; set; }
        [DisplayName("Sifre"), 
         StringLength(100, ErrorMessage = "{0} max.{1} karakter olmali"), 
         Required(ErrorMessage = "{0} alani gereklidir.")]
        public string Password { get; set; }
        
        
        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }
        
        public bool IsActive { get; set; }
        
        
        
        [Required, ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }

        public bool IsAdmin { get; set; }

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> likes { get; set; }


    }
}
