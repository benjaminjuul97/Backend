namespace FootballApplication.Tests;

[TestClass]
public class AuthenticationHelperTests
{
    [TestMethod]
    public void EncryptTest()
    {
        // Arrange
        string username = "john.doe";
        string password = "VerySecret!";

        // Act
        var header = AuthenticationHelper.Encrypt(username, password);

        // Assert
        //Assert.AreEqual("Basic am9obi5kb2U6VmVyeVNlY3JldCE=", header);
        Assert.AreEqual("Abc123!!!", header);

    }

    [TestMethod]
    public void DecryptTest() {
        // Arrange
        //string header = "Basic am9obi5kb2U6VmVyeVNlY3JldCE=";
        string header = "Abc123!!!";


        // Act
        AuthenticationHelper.Decrypt(header, out string username, out string password);

        // Assert
        Assert.AreEqual("john.doe", username);
        Assert.AreEqual("VerySecret!", password);
    }
}