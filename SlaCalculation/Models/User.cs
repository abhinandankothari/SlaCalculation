using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SlaCalculation.Models
{
    public class User
    {
    public int ID {get;set;} 
    public string Name{get;set;}
    public string EmployeeId { get; set; }
    public DateTime Dob{get; set;}
    public string Email{get;set;} 
    public string Mobile{get;set;}
    
    }
  
    public class UserDBContext:DbContext 
    {   
     public DbSet<User> Users {get;set;} 
     public void Save()
     {
         SaveChanges();

     }
     } 
    
}