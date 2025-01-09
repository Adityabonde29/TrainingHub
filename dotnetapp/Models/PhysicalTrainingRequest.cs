using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
    public class PhysicalTrainingRequest
    {
        public int PhysicalTrainingRequestId { get; set; }

        
        public int UserId { get; set; }
       [JsonIgnore] public User? User{get; set;}
        public int PhysicalTrainingId { get; set; }
        [JsonIgnore] public PhysicalTraining? PhysicalTraining {get; set;}
        public string RequestDate { get; set; }
        public string Status { get; set; }
        public string HealthConditions { get; set; }
        public string FitnessGoals { get; set; }
        public string? Comments { get; set; }
    }
}