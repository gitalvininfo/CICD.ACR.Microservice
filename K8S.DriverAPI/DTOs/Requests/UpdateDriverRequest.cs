﻿
namespace K8S.DriverAPI.DTOs.Requests
{
    public class UpdateDriverRequest
    {
        public Guid DriverId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int DriverNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}