using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Guest
    {
        [Key]
        public int GuestId {get;set;}
        //THE KEY OF A USER WHO IS A GUEST
        [Required]
        public int UserId {get;set;}
        //THE KEY OF A USER WHO IS A GUEST
        //RELATED NAME OF THE USER WHO IS A GUEST
        public User Attendee {get;set;}
        //RELATED NAME OF THE USER WHO IS A GUEST
        //THE KEY OF THE WEDDING TO ATTEND
        [Required]
        public int WeddingId {get;set;}
        //THE KEY OF THE WEDDING TO ATTEND
        //RELATED NAME OF THE WEDDING TO ATTEND
        public Wedding Reserved {get;set;}
        //RELATED NAME OF THE WEDDING TO ATTEND
    }        
}