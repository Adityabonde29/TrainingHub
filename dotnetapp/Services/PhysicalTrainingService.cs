using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Exceptions;
namespace dotnetapp.Services
{
    public class PhysicalTrainingService
    {
        private readonly ApplicationDbContext _context;
 
        public PhysicalTrainingService(ApplicationDbContext context)
        {
            _context = context;
        }
 
         public async Task<IEnumerable<PhysicalTraining>> GetAllPhysicalTrainings()
         {
            return await _context.PhysicalTrainings.ToListAsync();
         }
 
         public async Task<PhysicalTraining> GetPhysicalTrainingById(int trainingId)
         {
            return await _context.PhysicalTrainings.FindAsync(trainingId);
         }
 
        public async Task<bool> AddPhysicalTraining(PhysicalTraining training)
        {
            if (await _context.PhysicalTrainings.AnyAsync(pt => pt.TrainingName == training.TrainingName))
            {
                throw new PhysicalTrainingException("Training with the same name already exists.");
            }
 
            _context.PhysicalTrainings.Add(training);
            await _context.SaveChangesAsync();
            return true;
        }  
 
        public async Task<bool> UpdatePhysicalTraining(int trainingId, PhysicalTraining training)
        {
            var existingTraining = await _context.PhysicalTrainings.FindAsync(trainingId);
            if (existingTraining == null)
            {
                return false;
            }
 
            if (await _context.PhysicalTrainings.AnyAsync(pt => pt.TrainingName == training.TrainingName && pt.PhysicalTrainingId != trainingId))
            {
                throw new PhysicalTrainingException("Training with the same name already exists.");
            }
 
            existingTraining.TrainingName = training.TrainingName;
            existingTraining.Description = training.Description;
            existingTraining.TrainerName = training.TrainerName;
            existingTraining.Location = training.Location;
            existingTraining.IsIndoor = training.IsIndoor;
            existingTraining.Fee = training.Fee;
            existingTraining.FocusArea = training.FocusArea;
            existingTraining.PhysicalRequirements = training.PhysicalRequirements;
 
            await _context.SaveChangesAsync();
            return true;
        }
 
        public async Task<bool> DeletePhysicalTraining(int trainingId)
        {
            var training = await _context.PhysicalTrainings.FindAsync(trainingId);
            if (training == null)
            {
                return false;
            }
 
            if (await _context.PhysicalTrainingRequests.AnyAsync(ptr => ptr.PhysicalTrainingId == trainingId))
            {
                throw new PhysicalTrainingException("Training cannot be deleted as it is referenced in a request.");
            }
 
            _context.PhysicalTrainings.Remove(training);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}