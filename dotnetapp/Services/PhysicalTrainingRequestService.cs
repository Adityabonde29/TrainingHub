using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using dotnetapp.Data;


namespace dotnetapp.Services
{
    public class PhysicalTrainingRequestService
    {
        private readonly ApplicationDbContext _context;

        // public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        // {
        //     app.UseAuthentication();
        //     app.UseAuthorization();

        //     app.UseEndpoints(endpoints =>
        //     {
        //         endpoints.MapControllers();
        //     });
        // }

        public PhysicalTrainingRequestService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<PhysicalTrainingRequest>> GetAllPhysicalTrainingRequests()
        {
            return await _context.PhysicalTrainingRequests
                .Include(ptr => ptr.PhysicalTraining)
                .Include(ptr => ptr.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<PhysicalTrainingRequest>> GetPhysicalTrainingRequestsByUserId(int userId)
        {
            return await _context.PhysicalTrainingRequests
                .Include(ptr => ptr.PhysicalTraining)
                .Include(ptr => ptr.User)
                .Where(ptr => ptr.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> AddPhysicalTrainingRequest(PhysicalTrainingRequest request)
        {

            var existingRequest = await _context.PhysicalTrainingRequests
                .FirstOrDefaultAsync(ptr => (ptr.PhysicalTrainingId == request.PhysicalTrainingId && ptr.UserId==request.UserId));

            if (existingRequest != null)
            {
                throw new PhysicalTrainingException("User already requested this training.");
            }
            
            _context.PhysicalTrainingRequests.Add(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePhysicalTrainingRequest(int requestId, PhysicalTrainingRequest request)
        {
            var existingRequest = await _context.PhysicalTrainingRequests.FindAsync(requestId);
            if (existingRequest == null)
            {
                return false;
            }

            request.PhysicalTrainingRequestId = requestId;
            // existingRequest.UserId = request.UserId;
            // existingRequest.PhysicalTraining=request.PhysicalTraining;
            // existingRequest.RequestDate=request.RequestDate;
            // existingRequest.Status=request.Status;
            // existingRequest.HealthConditions=request.HealthConditions;
            // existingRequest.FitnessGoals=request.FitnessGoals;
            // existingRequest.Comments=request.Comments;

            // existingRequest.Status = request.Status;
            _context.Entry(existingRequest).CurrentValues.SetValues(request);


            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<bool> DeletePhysicalTrainingRequest(int requestId)
        {
            var existingRequest = await _context.PhysicalTrainingRequests.FindAsync(requestId);
            if (existingRequest == null)
            {
                return false;
            }

            _context.PhysicalTrainingRequests.Remove(existingRequest);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


