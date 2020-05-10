using Microsoft.AspNetCore.Identity;
using System;

namespace Blazor.SimpleTemplate.Services {
    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions {

        public EmailConfirmationTokenProviderOptions() {
            Name = "CustomEmailConfirmationTokenProvider";
            TokenLifespan = TimeSpan.FromHours(4);
        }
    }
}
