using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class VerificationCodeTest
{
    [Fact]
    public void ShouldGenerateVerificationCode()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.NotNull(verificationCode.Code);

        #endregion
    }

    [Fact]
    public void ShouldGenerateExpiresAtInFuture()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        var now = DateTime.UtcNow;
        dateTimeProviderMock.Setup(x => x.UtcNow).Returns(now);

        #endregion

        #region Act

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.True(verificationCode.ExpiresAtUtc > now);

        #endregion
    }

    [Fact]
    public void ShouldGenerateVerifiedAtAsNull()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.Null(verificationCode.VerifiedAtUtc);

        #endregion
    }

    [Fact]
    public void ShouldBeInactiveWhenCreated()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.False(verificationCode.IsActive);

        #endregion
    }

    [Fact]
    public void ShouldFailIfExpired()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock1 = new();
        dateTimeProviderMock1.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);

        Mock<IDateTimeProvider> dateTimeProviderMock2 = new();
        dateTimeProviderMock2.Setup(x => x.UtcNow).Returns(DateTime.UtcNow.AddHours(1));

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProviderMock1.Object);

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidVerificationCodeException>(() => verificationCode.ShouldVerify(verificationCode, dateTimeProviderMock2.Object));

        #endregion
    }

    [Fact]
    public void ShouldFailIfCodeIsInvalid()
    {
        #region Arrange

        string code = string.Empty;
        VerificationCode verificationCode = (VerificationCode)code;
        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidVerificationCodeException>(() => verificationCode.ShouldVerify(verificationCode.Code, dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldFailIfCodeIsLessThanSixChars()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        string code = Guid.NewGuid().ToString("N")[..4].ToUpper();
        VerificationCode verificationCode = (VerificationCode)code;

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidVerificationCodeException>(() => verificationCode.ShouldVerify(verificationCode.Code, dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldFailIfCodeIsGreaterThanSixChars()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        string code = Guid.NewGuid().ToString("N")[..10].ToUpper();
        VerificationCode verificationCode = (VerificationCode)code;

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidVerificationCodeException>(() => verificationCode.ShouldVerify(verificationCode.Code, dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldFailIfIsNotActive()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.False(verificationCode.IsActive);

        #endregion
    }

    [Fact]
    public void ShouldFailIfIsAlreadyVerified()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        var verificationCode = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);

        #endregion

        #region Act

        verificationCode.ShouldVerify(verificationCode.Code, dateTimeProviderMock.Object);

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidVerificationCodeException>(() => verificationCode.ShouldVerify(verificationCode.Code, dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();
        var verificationCode1 = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);
        var verificationCode2 = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);

        #endregion

        #region Act & Assert

        Assert.Throws<InvalidVerificationCodeException>(() => verificationCode2.ShouldVerify(verificationCode1.Code, dateTimeProviderMock.Object));

        #endregion
    }

    [Fact]
    public void ShouldVerify()
    {
        #region Arrange

        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        #endregion

        #region Act

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProviderMock.Object);
        verificationCode.ShouldVerify(verificationCode.Code, dateTimeProviderMock.Object);

        #endregion

        #region Assert

        Assert.NotNull(verificationCode.Code);

        #endregion
    }
}