

namespace CMS.Backend.Domain
{
    public static class Codes
    {
        public static string EmailInUse => "AUTH.ALERT.email_in_use";
        public static string InvalidCredentials => "AUTH.ALERT.INVALID_CREDENTIALS";
        public static string InvalidCurrentPassword => "AUTH.ALERT.invalid_current_password";
        public static string InvalidEmail => "AUTH.ALERT.invalid_email";
        public static string InvalidPassword => "AUTH.ALERT.INVALID_PASSWORD";
        public static string InvalidRole => "AUTH.ALERT.INVALID_ROLE";
        public static string RefreshTokenNotFound => "refresh_token_not_found";
        public static string RefreshTokenAlreadyRevoked => "refresh_token_already_revoked";
        public static string UserNotFound => "user_not_found";
        public static string NotActivated => "AUTH.ALERT.ACCOUNT_NOT_ACTIVATED";
        public static string AccountDeleted => "AUTH.ALERT.ACCOUNT_DELETED";


    }
}
