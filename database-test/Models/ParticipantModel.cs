using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace database_test.Models
{
    public class ParticipantModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [DataMember]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Intolerances { get; set; }
        public string Discipline { get; set; }
        [Required]
        public string Session { get; set; }
    }
}