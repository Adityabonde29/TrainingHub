using dotnetapp.Exceptions;
using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Reflection;
using dotnetapp.Services;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class Tests
    {

        private ApplicationDbContext _context; 
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _context = new ApplicationDbContext(options);
           
             _httpClient = new HttpClient();
             _httpClient.BaseAddress = new Uri("http://localhost:8080");

        }

        [TearDown]
        public void TearDown()
        {
             _context.Dispose();
        }

   [Test, Order(1)]
    public async Task Backend_Test_Post_Method_Register_Admin_Returns_HttpStatusCode_OK()
    {
        ClearDatabase();
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
  
   [Test, Order(2)]
    public async Task Backend_Test_Post_Method_Login_Admin_Returns_HttpStatusCode_OK()
    {
        ClearDatabase();

        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        // Print registration response
        string registerResponseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Registration Response: " + registerResponseBody);

        // Login with the registered user
        string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

        // Print login response
        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        Console.WriteLine("Login Response: " + loginResponseBody);

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }


[Test, Order(3)]
public async Task Backend_Test_Post_PhysicalTraining_With_Token_By_Admin_Returns_HttpStatusCode_OK()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Print login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Login Response: " + loginResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);
    string token = responseMap.token;

    Assert.IsNotNull(token);

    string uniqueTrainingName = $"training_{Guid.NewGuid().ToString()}";

    // Create a unique PhysicalTraining JSON payload
    string trainingJson = $"{{\"TrainingName\":\"{uniqueTrainingName}\",\"Description\":\"Test description\",\"TrainerName\":\"John Doe\",\"Location\":\"Gym\",\"IsIndoor\":true,\"Fee\":50,\"FocusArea\":\"Strength\",\"PhysicalRequirements\":\"Basic fitness level\",\"TrainerCertification\":\"Certified Trainer\"}}";
    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    
    HttpResponseMessage trainingResponse = await _httpClient.PostAsync("/api/physicalTraining",
        new StringContent(trainingJson, Encoding.UTF8, "application/json"));

    Console.WriteLine("Training Response: " + trainingResponse);

    Assert.AreEqual(HttpStatusCode.OK, trainingResponse.StatusCode);
}

[Test, Order(4)]

public async Task Backend_Test_Post_PhysicalTraining_Without_Token_By_Admin_Returns_HttpStatusCode_Unauthorized()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Print login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Login Response: " + loginResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    string uniqueTrainingName = $"training_{Guid.NewGuid().ToString()}";

    // Create a unique PhysicalTraining JSON payload
    string trainingJson = $"{{\"TrainingName\":\"{uniqueTrainingName}\",\"Description\":\"Test description\",\"TrainerName\":\"John Doe\",\"Location\":\"Gym\",\"IsIndoor\":true,\"Fee\":50,\"FocusArea\":\"Strength\",\"PhysicalRequirements\":\"Basic fitness level\",\"TrainerCertification\":\"Certified Trainer\"}}";
    HttpResponseMessage trainingResponse = await _httpClient.PostAsync("/api/physicalTraining",
        new StringContent(trainingJson, Encoding.UTF8, "application/json"));

    Console.WriteLine("Training Response: " + trainingResponse);

    Assert.AreEqual(HttpStatusCode.Unauthorized, trainingResponse.StatusCode);
}


[Test, Order(5)]
public async Task Backend_Test_Get_Method_Get_PhysicalTrainingById_In_PhysicalTraining_Service_Fetches_PhysicalTraining_Successfully()
{
    ClearDatabase();

    var trainingData = new Dictionary<string, object>
    {
        { "PhysicalTrainingId", 20 },
        { "TrainingName", "Strength Training" },
        { "Description", "Training for building strength" },
        { "TrainerName", "John Doe" },
        { "Location", "Gym" },
        { "IsIndoor", true },
        { "Fee", 50m },
        { "FocusArea", "Strength" },
        { "PhysicalRequirements", "Basic fitness level" },
        { "TrainerCertification", "Certified Trainer" }
    };

    var training = new PhysicalTraining();
    foreach (var kvp in trainingData)
    {
        var propertyInfo = typeof(PhysicalTraining).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(training, kvp.Value);
        }
    }
    _context.PhysicalTrainings.Add(training);
    _context.SaveChanges();

    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.PhysicalTrainingService";
    string typeName = "dotnetapp.Models.PhysicalTraining";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo getTrainingMethod = serviceType.GetMethod("GetPhysicalTrainingById");

    if (getTrainingMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var retrievedTraining = (Task<PhysicalTraining>)getTrainingMethod.Invoke(service, new object[] { 20 });

        Assert.IsNotNull(retrievedTraining);
        Assert.AreEqual(training.TrainingName, retrievedTraining.Result.TrainingName);
    }
    else
    {
        Assert.Fail();
    }
}

 [Test, Order(6)]
public async Task Backend_Test_Put_Method_UpdatePhysicalTraining_In_PhysicalTraining_Service_Updates_PhysicalTraining_Successfully()
{
    ClearDatabase();

    var trainingData = new Dictionary<string, object>
    {
        { "PhysicalTrainingId", 20 },
        { "TrainingName", "Strength Training" },
        { "Description", "A training for building strength" },
        { "TrainerName", "John Doe" },
        { "Location", "Gym" },
        { "IsIndoor", true },
        { "Fee", 50m },
        { "FocusArea", "Strength" },
        { "PhysicalRequirements", "Basic fitness level" },
        { "TrainerCertification", "Certified Trainer" }
    };

    var training = new PhysicalTraining();
    foreach (var kvp in trainingData)
    {
        var propertyInfo = typeof(PhysicalTraining).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(training, kvp.Value);
        }
    }
    _context.PhysicalTrainings.Add(training);
    _context.SaveChanges();

    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.PhysicalTrainingService";
    string typeName = "dotnetapp.Models.PhysicalTraining";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo updateMethod = serviceType.GetMethod("UpdatePhysicalTraining", new[] { typeof(int), modelType });

    if (updateMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);

        var updatedTrainingData = new Dictionary<string, object>
        {
            { "PhysicalTrainingId", 20 },
            { "TrainingName", "Advanced Strength Training" },
            { "Description", "Updated training for building strength" },
            { "TrainerName", "Jane Doe" },
            { "Location", "Studio" },
            { "IsIndoor", false },
            { "Fee", 55m },
            { "FocusArea", "Strength" },
            { "PhysicalRequirements", "Intermediate fitness level" },
            { "TrainerCertification", "Senior Certified Trainer" }
        };

        var updatedTraining = Activator.CreateInstance(modelType);
        foreach (var kvp in updatedTrainingData)
        {
            var propertyInfo = modelType.GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(updatedTraining, kvp.Value);
            }
        }

        var updateResult = (Task<bool>)updateMethod.Invoke(service, new object[] { 20, updatedTraining });

        var updatedTrainingFromDb = await _context.PhysicalTrainings.FindAsync(20);
        Assert.IsNotNull(updatedTrainingFromDb);
        Assert.AreEqual("Advanced Strength Training", updatedTrainingFromDb.TrainingName);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(7)]
public async Task Backend_Test_Delete_Method_DeletePhysicalTraining_In_PhysicalTraining_Service_Deletes_PhysicalTraining_Successfully()
{
    ClearDatabase();

    var trainingData = new Dictionary<string, object>
    {
        { "PhysicalTrainingId", 4 },
        { "TrainingName", "Strength Training" },
        { "Description", "A training for building strength" },
        { "TrainerName", "John Doe" },
        { "Location", "Gym" },
        { "IsIndoor", true },
        { "Fee", 50m },
        { "FocusArea", "Strength" },
        { "PhysicalRequirements", "Basic fitness level" },
        { "TrainerCertification", "Certified Trainer" }
    };

    var training = new PhysicalTraining();
    foreach (var kvp in trainingData)
    {
        var propertyInfo = typeof(PhysicalTraining).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(training, kvp.Value);
        }
    }

    _context.PhysicalTrainings.Add(training);
    _context.SaveChanges();

    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.PhysicalTrainingService";
    string typeName = "dotnetapp.Models.PhysicalTraining";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo deleteMethod = serviceType.GetMethod("DeletePhysicalTraining", new[] { typeof(int) });

    if (deleteMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var deleteResult = (Task<bool>)deleteMethod.Invoke(service, new object[] { 4 });

        var deletedTrainingFromDb = await _context.PhysicalTrainings.FindAsync(4);
        Assert.IsNull(deletedTrainingFromDb);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(8)]
public async Task Backend_Test_Post_Method_AddPhysicalTrainingRequest_In_PhysicalTrainingRequest_Service_Posts_Successfully()
{
    ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId", 400 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Add physical training
    var trainingData = new Dictionary<string, object>
    {
        { "PhysicalTrainingId", 100 },
        { "TrainingName", "Yoga Class" },
        { "Description", "A yoga class for relaxation and flexibility" },
        { "TrainerName", "John Doe" },
        { "Location", "Yoga Studio" },
        { "IsIndoor", true },
        { "Fee", 25m },
        { "FocusArea", "Flexibility" },
        { "PhysicalRequirements", "Basic fitness level" },
        { "TrainerCertification", "Certified Yoga Trainer" }
    };

    var training = new PhysicalTraining();
    foreach (var kvp in trainingData)
    {
        var propertyInfo = typeof(PhysicalTraining).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(training, kvp.Value);
        }
    }
    _context.PhysicalTrainings.Add(training);
    _context.SaveChanges();

    // Add physical training request
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.PhysicalTrainingRequestService";
    string typeName = "dotnetapp.Models.PhysicalTrainingRequest";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("AddPhysicalTrainingRequest", new[] { modelType });

    if (method != null)
    {
        var trainingRequestData = new Dictionary<string, object>
        {
            { "PhysicalTrainingRequestId", 200 },
            { "UserId", 400 },
            { "PhysicalTrainingId", 100 },
            { "RequestDate", DateTime.Now.ToString() },
            { "Status", "Pending" },
            { "HealthConditions", "Asthma" },
            { "FitnessGoals", "Increase flexibility" },
            { "Comments", "Looking forward to the session" }
        };

        var trainingRequest = Activator.CreateInstance(modelType);
        foreach (var kvp in trainingRequestData)
        {
            var propertyInfo = modelType.GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(trainingRequest, kvp.Value);
            }
        }
        var service = Activator.CreateInstance(serviceType, _context);
        var result = (Task<bool>)method.Invoke(service, new object[] { trainingRequest });
    
        var addedTrainingRequest = await _context.PhysicalTrainingRequests.FindAsync(200);
        Assert.IsNotNull(addedTrainingRequest);
        Assert.AreEqual("Pending", addedTrainingRequest.Status);
        Assert.AreEqual("Asthma", addedTrainingRequest.HealthConditions);
        Assert.AreEqual("Increase flexibility", addedTrainingRequest.FitnessGoals);
        Assert.AreEqual("Looking forward to the session", addedTrainingRequest.Comments);
    }
    else
    {  
        Assert.Fail();
    }
}

[Test, Order(9)]
public async Task Backend_Test_Get_Method_GetPhysicalTrainingRequestByUserId_In_PhysicalTrainingRequest_Fetches_Successfully()
{
    ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId", 400 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Add physical training
    var trainingData = new Dictionary<string, object>
    {
        { "PhysicalTrainingId", 100 },
        { "TrainingName", "Yoga Class" },
        { "Description", "A yoga class for relaxation and flexibility" },
        { "TrainerName", "John Doe" },
        { "Location", "Yoga Studio" },
        { "IsIndoor", true },
        { "Fee", 25m },
        { "FocusArea", "Flexibility" },
        { "PhysicalRequirements", "Basic fitness level" },
        { "TrainerCertification", "Certified Yoga Trainer" }
    };

    var training = new PhysicalTraining();
    foreach (var kvp in trainingData)
    {
        var propertyInfo = typeof(PhysicalTraining).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(training, kvp.Value);
        }
    }
    _context.PhysicalTrainings.Add(training);
    _context.SaveChanges();

    // Add physical training request
    var requestData = new Dictionary<string, object>
    {
        { "PhysicalTrainingRequestId", 200 },
        { "UserId", 400 },
        { "PhysicalTrainingId", 100 },
        { "RequestDate", DateTime.Now.ToString() },
        { "Status", "Pending" },
        { "HealthConditions", "Asthma" },
        { "FitnessGoals", "Increase flexibility" },
        { "Comments", "Looking forward to the session" }
    };

    var request = new PhysicalTrainingRequest();
    foreach (var kvp in requestData)
    {
        var propertyInfo = typeof(PhysicalTrainingRequest).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(request, kvp.Value);
        }
    }
    _context.PhysicalTrainingRequests.Add(request);
    _context.SaveChanges();

    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.PhysicalTrainingRequestService";
    string typeName = "dotnetapp.Models.PhysicalTrainingRequest";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("GetPhysicalTrainingRequestsByUserId");

    if (method != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var result = (Task<IEnumerable<PhysicalTrainingRequest>>)method.Invoke(service, new object[] { 400 });
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Result.Any(item => item.Comments == "Looking forward to the session"));
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(10)]
public async Task Backend_Test_Put_Method_Update_In_PhysicalTrainingRequest_Service_Updates_Successfully()
{
    ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId", 400 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Add physical training
    var trainingData = new Dictionary<string, object>
    {
        { "PhysicalTrainingId", 100 },
        { "TrainingName", "Yoga Class" },
        { "Description", "A yoga class for relaxation and flexibility" },
        { "TrainerName", "John Doe" },
        { "Location", "Yoga Studio" },
        { "IsIndoor", true },
        { "Fee", 25m },
        { "FocusArea", "Flexibility" },
        { "PhysicalRequirements", "Basic fitness level" },
        { "TrainerCertification", "Certified Yoga Trainer" }
    };

    var training = new PhysicalTraining();
    foreach (var kvp in trainingData)
    {
        var propertyInfo = typeof(PhysicalTraining).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(training, kvp.Value);
        }
    }
    _context.PhysicalTrainings.Add(training);
    _context.SaveChanges();

    // Add initial physical training request
    var requestData = new Dictionary<string, object>
    {
        { "PhysicalTrainingRequestId", 200 },
        { "UserId", 400 },
        { "PhysicalTrainingId", 100 },
        { "RequestDate", DateTime.Now.ToString() },
        { "Status", "Pending" },
        { "HealthConditions", "Asthma" },
        { "FitnessGoals", "Increase flexibility" },
        { "Comments", "Looking forward to the session" }
    };

    var request = new PhysicalTrainingRequest();
    foreach (var kvp in requestData)
    {
        var propertyInfo = typeof(PhysicalTrainingRequest).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(request, kvp.Value);
        }
    }
    _context.PhysicalTrainingRequests.Add(request);
    _context.SaveChanges();

    // Update request data
    var updatedRequestData = new Dictionary<string, object>
    {
        { "PhysicalTrainingRequestId", 200 },
        { "UserId", 400 },
        { "PhysicalTrainingId", 100 },
        { "RequestDate", DateTime.Now.ToString() },
        { "Status", "Approved" },
        { "HealthConditions", "None" },
        { "FitnessGoals", "Build muscle" },
        { "Comments", "Updated request comments" }
    };

    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.PhysicalTrainingRequestService";
    string typeName = "dotnetapp.Models.PhysicalTrainingRequest";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("UpdatePhysicalTrainingRequest", new[] { typeof(int), modelType });

    if (method != null)
    {
        var updatedRequest = Activator.CreateInstance(modelType);
        foreach (var kvp in updatedRequestData)
        {
            var propertyInfo = typeof(PhysicalTrainingRequest).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(updatedRequest, kvp.Value);
            }
        }

        var service = Activator.CreateInstance(serviceType, _context);
        var updateResult = (Task<bool>)method.Invoke(service, new object[] { 200, updatedRequest });
        var updatedRequestFromDb = await _context.PhysicalTrainingRequests.FindAsync(200);
        Assert.IsNotNull(updatedRequestFromDb);
        Assert.AreEqual("Approved", updatedRequestFromDb.Status);
        Assert.AreEqual("Updated request comments", updatedRequestFromDb.Comments);
    }
    else
    {
        Assert.Fail();
    }
}


[Test, Order(11)]
public async Task Backend_Test_Delete_Method_DeletePhysicalTrainingRequest_Service_Deletes_PhysicalTrainingRequest_Successfully()
{
    ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId", 32 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Add physical training request
    var trainingRequestData = new Dictionary<string, object>
    {
        { "PhysicalTrainingRequestId", 200 },
        { "UserId", 32 },
        { "PhysicalTrainingId", 100 },
        { "RequestDate", DateTime.Now.ToString() },
        { "Status", "Pending" },
        { "HealthConditions", "Asthma" },
        { "FitnessGoals", "Increase flexibility" },
        { "Comments", "Looking forward to the session" }
    };

    var request = new PhysicalTrainingRequest();
    foreach (var kvp in trainingRequestData)
    {
        var propertyInfo = typeof(PhysicalTrainingRequest).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(request, kvp.Value);
        }
    }
    _context.PhysicalTrainingRequests.Add(request);
    _context.SaveChanges();

    // Delete request
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.PhysicalTrainingRequestService";
    string typeName = "dotnetapp.Models.PhysicalTrainingRequest";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo deleteMethod = serviceType.GetMethod("DeletePhysicalTrainingRequest", new[] { typeof(int) });

    if (deleteMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var deleteResult = (Task<bool>)deleteMethod.Invoke(service, new object[] { 200 });

        var deletedRequestFromDb = await _context.PhysicalTrainingRequests.FindAsync(200);
        Assert.IsNull(deletedRequestFromDb);
    }
    else
    {
        Assert.Fail();
    }
}
[Test, Order(12)]
public async Task Backend_Test_Post_Method_AddFeedback_In_Feedback_Service_Posts_Successfully()
{
        ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId",42 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();
    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("AddFeedback", new[] { modelType });

    if (method != null)
    {
           var feedbackData = new Dictionary<string, object>
            {
                { "FeedbackId", 11 },
                { "UserId", 42 },
                { "FeedbackText", "Great experience!" },
                { "Date", DateTime.Now }
            };
        var feedback = new Feedback();
        foreach (var kvp in feedbackData)
        {
            var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(feedback, kvp.Value);
            }
        }
        var service = Activator.CreateInstance(serviceType, _context);
        var result = (Task<bool>)method.Invoke(service, new object[] { feedback });
    
        var addedFeedback= await _context.Feedbacks.FindAsync(11);
        Assert.IsNotNull(addedFeedback);
        Assert.AreEqual("Great experience!",addedFeedback.FeedbackText);

    }
    else{
        Assert.Fail();
    }
}

[Test, Order(13)]
public async Task Backend_Test_Delete_Method_Feedback_In_Feeback_Service_Deletes_Successfully()
{
    // Add user
     ClearDatabase();

    var userData = new Dictionary<string, object>
    {
        { "UserId",42 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

           var feedbackData = new Dictionary<string, object>
            {
                { "FeedbackId", 11 },
                { "UserId", 42 },
                { "FeedbackText", "Great experience!" },
                { "Date", DateTime.Now }
            };
        var feedback = new Feedback();
        foreach (var kvp in feedbackData)
        {
            var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(feedback, kvp.Value);
            }
        }
     _context.Feedbacks.Add(feedback);
    _context.SaveChanges();
    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

  
    MethodInfo deletemethod = serviceType.GetMethod("DeleteFeedback", new[] { typeof(int) });

    if (deletemethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var deleteResult = (Task<bool>)deletemethod.Invoke(service, new object[] { 11 });

        var deletedFeedbackFromDb = await _context.Feedbacks.FindAsync(11);
        Assert.IsNull(deletedFeedbackFromDb);
    }
    else
    {
        Assert.Fail();
    }
}
[Test, Order(14)]
public async Task Backend_Test_Get_Method_GetFeedbacksByUserId_In_Feedback_Service_Fetches_Successfully()
{
    ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId", 330 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    var feedbackData= new Dictionary<string, object>
    {
        { "FeedbackId", 13 },
        { "UserId", 330 },
        { "FeedbackText", "Great experience!" },
        { "Date", DateTime.Now }
    };

    var feedback = new Feedback();
    foreach (var kvp in feedbackData)
    {
        var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(feedback, kvp.Value);
        }
    }
    _context.Feedbacks.Add(feedback);
    _context.SaveChanges();

    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("GetFeedbacksByUserId");

    if (method != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var result = ( Task<IEnumerable<Feedback>>)method.Invoke(service, new object[] {330});
        Assert.IsNotNull(result);
         var check=true;
        foreach (var item in result.Result)
        {
            check=false;
            Assert.AreEqual("Great experience!", item.FeedbackText);
   
        }
        if(check==true)
        {
            Assert.Fail();

        }
    }
    else{
        Assert.Fail();
    }
}
[Test, Order(15)]


public async Task Backend_Test_Post_Method_AddPhysicalTraining_In_PhysicalTrainingService_Throws_Exception_For_Duplicate_TrainingName()
{
    ClearDatabase();

    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.PhysicalTrainingService";
    string typeName = "dotnetapp.Models.PhysicalTraining";
 
    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);
 
    MethodInfo method = serviceType.GetMethod("AddPhysicalTraining", new[] { modelType });
 
    if (method != null)
    {
        // Add initial PhysicalTraining with a unique TrainingName
        var trainingData = new Dictionary<string, object>
        {
            { "PhysicalTrainingId", 1 },
            { "TrainingName", "Strength Training" },
            { "Description", "Training focused on building strength" },
            { "TrainerName", "John Doe" },
            { "Location", "Gym" },
            { "IsIndoor", true },
            { "Fee", 100m },
            { "FocusArea", "Strength" },
            { "PhysicalRequirements", "Basic fitness level" },
            { "TrainerCertification", "Certified Trainer" }
        };

        var training = Activator.CreateInstance(modelType);
        foreach (var kvp in trainingData)
        {
            var propertyInfo = modelType.GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(training, kvp.Value);
            }
        }

        var service = Activator.CreateInstance(serviceType, _context);
        var result = (Task<bool>)method.Invoke(service, new object[] { training });
        var addedTraining = await _context.PhysicalTrainings.FindAsync(1);
        Assert.IsNotNull(addedTraining);

        // Attempt to add another PhysicalTraining with the same TrainingName
        var duplicateTrainingData = new Dictionary<string, object>
        {
            { "PhysicalTrainingId", 2 },
            { "TrainingName", "Strength Training" }, // Duplicate name
            { "Description", "Another strength training session" },
            { "TrainerName", "Jane Smith" },
            { "Location", "Park" },
            { "IsIndoor", false },
            { "Fee", 80m },
            { "FocusArea", "Strength" },
            { "PhysicalRequirements", "Basic fitness level" },
            { "TrainerCertification", "Certified Trainer" }
        };

        var duplicateTraining = Activator.CreateInstance(modelType);
        foreach (var kvp in duplicateTrainingData)
        {
            var propertyInfo = modelType.GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(duplicateTraining, kvp.Value);
            }
        }

        try
        {
            var duplicateResult = (Task<bool>)method.Invoke(service, new object[] { duplicateTraining });
            Console.WriteLine("Result: " + duplicateResult.Result);
            Assert.Fail("Expected PhysicalTrainingException was not thrown.");
        }
        catch (Exception ex)
        {
            Assert.IsNotNull(ex.InnerException);
            Assert.IsTrue(ex.InnerException is PhysicalTrainingException);
        }
    }
    else
    {
        Assert.Fail("Method AddPhysicalTraining not found in PhysicalTrainingService.");
    }
}


private void ClearDatabase()
{
    _context.Database.EnsureDeleted();
    _context.Database.EnsureCreated();
}

}
}