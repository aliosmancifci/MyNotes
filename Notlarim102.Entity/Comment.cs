using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblComments")]
    public class Comment:MyEntityBase
    {
        [StringLength(300), Required]
        public string Text { get; set; }

       
        public int NoteId { get; set; }

        public int? OwnerId { get; set; }

        public virtual Note Note { get; set; }
        public virtual NotlarimUser Owner { get; set; }


    }
}
