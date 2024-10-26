using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.AccountContext.ValueObjects;

public class VerificationCode
{
    #region Constants

    private const int MinLength = 6;

    #endregion

    #region Constructors

    public VerificationCode(string code)
    {
        Code = code;
    }

    private VerificationCode(string code, DateTime expiresAtUtc)
    {
        Code = code;
        ExpiresAtUtc = expiresAtUtc;
    }

    #endregion

    #region Factories

    public static VerificationCode ShouldCreate(IDateTimeProvider dateTimeProvider) =>
        new(
            Guid.NewGuid().ToString("N")[..MinLength].ToUpper(), 
            dateTimeProvider.UtcNow.AddMinutes(5));

    #endregion

    #region Properties

    public string Code { get; }
    public DateTime? ExpiresAtUtc { get; private set; }
    public DateTime? VerifiedAtUtc { get; private set; }
    public bool IsActive => VerifiedAtUtc != null && ExpiresAtUtc is null;

    #endregion

    #region Methods

    public void ShouldVerify(string code, IDateTimeProvider dateTimeProvider)
    {
        if (string.IsNullOrEmpty(code))
            throw new InvalidVerificationCodeException();
        
        if (string.IsNullOrWhiteSpace(code))
            throw new InvalidVerificationCodeException();
        
        if(code.Length != MinLength)
            throw new InvalidVerificationCodeException();

        if (Code != code)
            throw new InvalidVerificationCodeException();

        if (VerifiedAtUtc != null)
            throw new InvalidVerificationCodeException("This code is already verified");

        if (ExpiresAtUtc < dateTimeProvider.UtcNow)
            throw new InvalidVerificationCodeException("This code is expiried");

        VerifiedAtUtc = DateTime.UtcNow;
        ExpiresAtUtc = null;
    }

    #endregion
    
    #region Operators
    
    public static implicit operator string(VerificationCode verificationCode) => verificationCode.ToString();

    public static implicit operator VerificationCode(string code) => new VerificationCode(code);
    
    #endregion

    #region Others

    public override string ToString() => Code;

    #endregion
}