﻿namespace CodeVote.DTO
{
    public class ReadUserDTO
    {
        public Guid UserId { get; set; }           
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
