﻿namespace SoccerManager.Api.Shared.Model
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public long Value { get; set; }
    }
}
