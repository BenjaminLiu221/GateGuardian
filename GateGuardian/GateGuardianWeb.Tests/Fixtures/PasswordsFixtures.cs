using GateGuardianWeb.Models;
using System;
using System.Collections.Generic;


namespace GateGuardianWeb.Tests.Fixtures
{
    public static class PasswordsFixtures
    {
        public static List<Password> GetTestPasswords() => new()
        {
            new Password
            {
                Id = 1,
                Characters = "badpw123"
            },
            new Password
            {
                Id = 2,
                Characters = "OhMyGod!Amaz1ngPa$$word."
            },
            new Password
            {
                Id = 3,
                Characters = "anynumbershere"
            },
        };
    }
}
