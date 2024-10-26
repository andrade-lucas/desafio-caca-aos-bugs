using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    private const string ValidPassword = "1WBD=43j@k9Oh#wk";

    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        string? passStr = null;

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(passStr, dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        string passStr = string.Empty;

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(passStr, dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        string passStr = "   ";

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(passStr, dateTimeProviderMock.Object));

        #endregion
    }

    [Theory]
    [InlineData("p")]
    [InlineData("pas")]
    [InlineData("#mYt3s")]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars(string value)
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(value, dateTimeProviderMock.Object));

        #endregion
    }

    [Theory]
    [InlineData("k;$)>oadeUc]E£m[LOJLlqPQ'iN`w4b:_I/=3f*0&po8n!Q2d8")]
    [InlineData("lj!p'E25,><),lu*qKx}A-vJ^kHn(zfb=3C+S)#MJn4a03t:!<")]
    [InlineData(",yD~1_T2qyaGzOZcv))qx)Ph\\NZ'vEH[y0On<6W]&+-15y+kP)")]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars(string value)
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(value, dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldHashPassword()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var pass = Password.ShouldCreate(ValidPassword, dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.True(pass.Hash.Length > 0);

        #endregion
    }

    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var pass = Password.ShouldCreate(ValidPassword, dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.True(pass.Hash.Length > 0);

        #endregion
    }

    [Fact]
    public void ShouldGenerateStrongPassword()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var strongPass = Password.ShouldGenerate();

        #endregion

        #region Assert

        Assert.True(strongPass.Length > 0);

        #endregion
    }

    [Fact]
    public void ShouldImplicitConvertToString()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var pass = Password.ShouldCreate(ValidPassword, dateTimeProviderMock.Object);
        string strPass = pass;

        #endregion

        #region Assert

        Assert.Equal(pass.Hash, strPass);

        #endregion
    }

    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var pass = Password.ShouldCreate(ValidPassword, dateTimeProviderMock.Object);
        string strPass = pass.ToString();

        #endregion

        #region Assert

        Assert.Equal(pass.Hash, strPass);

        #endregion
    }

    [Fact]
    public void ShouldMarkPasswordAsExpired()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        dateTimeProviderMock.Setup(dt => dt.UtcNow).Returns(DateTime.UtcNow.AddDays(-1));

        #endregion

        #region Act

        var pass = Password.ShouldCreate(ValidPassword, dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.True(pass.IsExpiried);

        #endregion
    }

    [Fact]
    public void ShouldFailIfPasswordIsExpired()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        dateTimeProviderMock.Setup(dt => dt.UtcNow).Returns(DateTime.UtcNow.AddDays(-1));

        #endregion

        #region Act

        var pass = Password.ShouldCreate(ValidPassword, dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.Throws<InvalidPasswordException>(() => pass.ShouldVerify(dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldMarkPasswordAsMustChange()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        dateTimeProviderMock.Setup(dt => dt.UtcNow).Returns(DateTime.UtcNow.AddDays(-1));

        #endregion

        #region Act

        var pass = Password.ShouldCreate(ValidPassword, dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.True(pass.MustChange);

        #endregion
    }

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        dateTimeProviderMock.Setup(dt => dt.UtcNow).Returns(DateTime.UtcNow.AddDays(-1));

        #endregion

        #region Act

        var pass = Password.ShouldCreate(ValidPassword, dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.Throws<InvalidPasswordException>(() => pass.ShouldVerify(dateTimeProviderMock.Object));

        #endregion
    }
}