using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("batgirl@test.com")]
    [InlineData("robin@test.com")]
    [InlineData("batman@test.com")]
    public void ShouldLowerCaseEmail(string emailAddress)
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProvider = new();

        #endregion

        #region Act

        Email email = Email.ShouldCreate(emailAddress, dateTimeProvider.Object);

        #endregion

        #region Assert
        Assert.Equal(emailAddress, email.ToString());
        #endregion
    }

    [Fact]
    public void ShouldTrimEmail()
    {
        #region Arrange

        var notTrimEmail = "    batman@test.com    ";
        Mock<IDateTimeProvider> dateTimeProvider = new();

        #endregion

        #region Act

        Email email = Email.ShouldCreate(notTrimEmail, dateTimeProvider.Object);

        #endregion

        #region Assert

        Assert.Equal(notTrimEmail.Trim(), email.ToString());

        #endregion
    }

    [Fact]
    public void ShouldFailIfEmailIsNull()
    {
        #region Arrange

        string? nullEmail = null;
        Mock<IDateTimeProvider> dateTimeProvider = new();
        
        #endregion

        #region Act & Assert

        Assert.Throws<NullReferenceException>(() => Email.ShouldCreate(nullEmail, dateTimeProvider.Object));

        #endregion
    }

    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {
        #region Arrange

        string? emptyEmail = string.Empty;
        Mock<IDateTimeProvider> dateTimeProvider = new();
        
        #endregion

        #region Act & Assert

        Assert.Throws<InvalidEmailException>(() => Email.ShouldCreate(emptyEmail, dateTimeProvider.Object));

        #endregion
    }

    [Theory]
    [InlineData("invalid.com")]
    [InlineData("invalid")]
    [InlineData("something here")]
    public void ShouldFailIfEmailIsInvalid(string invalidEmail)
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProvider = new();
        
        #endregion

        #region Act & Assert

        Assert.Throws<InvalidEmailException>(() => Email.ShouldCreate(invalidEmail, dateTimeProvider.Object));

        #endregion
    }

    [Theory]
    [InlineData("batgirl@personal.com")]
    [InlineData("robin@gmail.com")]
    [InlineData("batman@outlook.com")]
    public void ShouldPassIfEmailIsValid(string validEmail)
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProvider = new();
        dateTimeProvider.Setup(m => m.UtcNow).Returns(DateTime.UtcNow);

        #endregion

        #region Act

        Email email = Email.ShouldCreate(validEmail, dateTimeProvider.Object);
        email.ShouldVerify(email.VerificationCode);

        #endregion

        #region Assert

        Assert.NotNull(email);

        #endregion
    }

    [Theory]
    [InlineData("batgirl@personal.com")]
    public void ShouldHashEmailAddress(string validEmail)
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProvider = new();

        #endregion

        #region Act

        Email email = Email.ShouldCreate(validEmail, dateTimeProvider.Object);

        #endregion

        #region Assert

        Assert.NotNull(email.Hash);

        #endregion
    }

    [Fact]
    public void ShouldExplicitConvertFromString()
    {
        #region Arrange

        const string strEmailConst = "test@test.com";

        #endregion

        #region Act

        Email email = (Email)strEmailConst;
        string strEmail = (string)email;

        #endregion

        #region Assert

        Assert.Equal(strEmailConst, strEmail);

        #endregion
    }

    [Fact]
    public void ShouldExplicitConvertToString()
    {
        #region Arrange

        const string strEmailConst = "test@test.com";
        Mock<IDateTimeProvider> dateTimeProvider = new();
        
        #endregion

        #region Act

        Email email = Email.ShouldCreate(strEmailConst, dateTimeProvider.Object);
        string strEmail = (string)email;

        #endregion

        #region Assert

        Assert.Equal(strEmailConst, strEmail);

        #endregion
    }

    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod()
    {
        #region Arrange

        const string strEmailTest = "test@test.com";
        Mock<IDateTimeProvider> dateTimeProvider = new();
        
        #endregion

        #region Act

        Email email = Email.ShouldCreate(strEmailTest, dateTimeProvider.Object);
        string strEmail = email.ToString();

        #endregion

        #region Assert

        Assert.Equal(strEmailTest, strEmail);

        #endregion
    }
}