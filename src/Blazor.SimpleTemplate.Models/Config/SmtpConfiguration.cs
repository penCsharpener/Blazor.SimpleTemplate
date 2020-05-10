namespace Blazor.SimpleTemplate.Models.Config {
    public class SmtpConfiguration {
        public string ServerUrl { get; set; }
        public int Port { get; set; }
        public bool RemoveXOAuth2 { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string RegistrationSubject { get; set; } = "Confirm your registration";
        public string RegistrationBodyTemplate { get; set; } = "Hi {name},<br><br>You have registered at {url}. " +
            "Please click below link to confirm your" +
            "registration.<br><br><a href=\"{link}\">Confirm your account creation</a><br><br>Thank you.<br>";
        public string PwdRestSubject { get; set; } = "Forgotten your password?";
        public string PwdRestBodyTemplate { get; set; } = "Hi {name},<br><br>Did you forget your {url} password. " +
            "If so, please click below link to choose" +
            "a new password.<br><br><a href=\"{link}\">Reset password</a>" +
            "<br><br>If you didn't trigger a password request, please ignore this email." +
            "<br><br>Thank you.<br>";

        public AuthMessageSenderOptions AuthMessageSenderOptions { get; set; }
        public int TokenLifespanFromHours { get; set; }
    }
}
