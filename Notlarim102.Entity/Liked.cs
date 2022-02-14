using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblLikeds")]
    public class Liked
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public int NoteId { get; set; }
        
        public int? LikedUserId { get; set; }

        public virtual Note Note { get; set; }
        public virtual NotlarimUser LikedUser { get; set; }

    }
}
