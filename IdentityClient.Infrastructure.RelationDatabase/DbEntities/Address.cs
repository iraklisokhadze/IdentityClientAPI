﻿using System;

namespace IdentityClient.Infrastructure.RelationDatabase
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Apartment { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
