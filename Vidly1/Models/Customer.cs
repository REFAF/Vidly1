using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly1.Models
{
    public class Customer
    {
        public int Id { get; set; }

        //28  override the default conventions. apply a data annotation or an attribute called required means it will no longer be nullable (need amespace).
        [Required]
        [StringLength(255)] //So this approach of overriding default conventions is called data annotations
        public string Name { get; set; }
        public bool IsSubscribedToNewaLetter { get; set; }

        //26  associate Customer class with MembershipType class (navigation property).
        public MembershipType MembershipType { get; set; }

        //26  you may only need the foreign key, Entity framework recognizes this convention and treats this property as a foreign key.
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]//53
        public DateTime? Birthdate { get; set; } //ex3.2
    }
}