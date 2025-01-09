using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Models
{
    public class PhysicalTraining
    {
        public int PhysicalTrainingId {get; set;}
        public string TrainingName {get; set;}
        public string Description {get; set;}
        public string TrainerName {get; set;}
        public string Location {get; set;}
        public bool IsIndoor {get; set;}
        public decimal Fee {get; set;}
        public string FocusArea {get; set;}
        public string PhysicalRequirements {get; set;}
    }
}