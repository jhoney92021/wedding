using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
        //LIST OF PROPERTIES
        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int UserId { get; set; }
        //FIRST NAME
        [Required]
        [Display(Name = "Your First Name:")]
        public string Fname {get;set;}
        //FIRST NAME
        //LAST NAME
        [Required]
        [Display(Name = "Your Last Name:")]
        public string Lname {get;set;}
        //LAST NAME
        //EMAIL
        [EmailAddress]
        [Required]
        [Display(Name = "Your Email:")]
        //EMAIL
        public string Email {get;set;}
        //PASSWORD
        [DataType(DataType.Password)]
        [Required] 
        [Display(Name = "Your Password:")]
        [MinLength(3)]
        public string Password {get;set;}
        //PASSWORD
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        //PASSWORD CONFIRMATION
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword{get;set;}
        //PASSWORD CONFIRMATION
        //LIST OF PROPERTIES
    }       
}