using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }
        [Required]
        [Display(Name = "Grooms Name:")]
        public string Groom {get;set;}
        [Required]
        [Display(Name = "Brides Name:")]
        public string Bride {get;set;}
        [Required]
        [Display(Name = "Wedding Date:")]
        public DateTime Date {get;set;}
        [Required]
        [Display(Name = "Wedding Location:")]
        public string Location {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        //KEY OF PLANNER
        public int UserId {get;set;}
        //KEY OF PLANNER
        //RELATED NAME
        public User Planner {get;set;}
        //RELATED NAME
        //LIST OF GUESTS
        public List<Guest> Guests{get;set;}
        //LIST OF GUESTS

    }
}