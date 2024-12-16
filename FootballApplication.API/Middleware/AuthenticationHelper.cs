using System.Text;

public class AuthenticationHelper {
    public static string Encrypt(string username, string password) {
        // 1. Concatenate credentials with a ':'
        string credentials = $"{username}:{password}";

        // 2. Retrieve bytes from text
        byte[] bytes = Encoding.UTF8.GetBytes(credentials);

        // 3. Base64 encode credentials
        string encryptedCredentials = Convert.ToBase64String(bytes);

        // 4. Prefix credentials with 'Basic' and return
        return $"Basic {encryptedCredentials}";
    }
    public static void Decrypt(string encryptedHeader, out string username, out string password) {
        // 1. Extract the username and password from the value by splitting it on space,
        // as the value looks something like 'Basic am9obi5kb2U6VmVyeVNlY3JldCE='
        var auth = encryptedHeader.Split(new[] { ' ' })[1];

        // 2. Convert it form Base64 encoded text, back to normal text
        var usernameAndPassword = Encoding.UTF8.GetString(Convert.FromBase64String(auth));

        // 3. Extract username and password, which are separated by a semicolon
        username = usernameAndPassword.Split(new[] {':'})[0];
        password = usernameAndPassword.Split(new[] {':'})[1];
    }
}