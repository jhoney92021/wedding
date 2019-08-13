using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class LoginUser
    {
    [Required]
    public string Email{get;set;}

    [Required]
    public string Password{get;set;}
    }        
}