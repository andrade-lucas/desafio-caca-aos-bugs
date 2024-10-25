using Balta.Domain.SharedContext.Extensions;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("ShouldGenerateBase64FromString1")]
    [InlineData("ShouldGenerateBase64FromString2")]
    [InlineData("ShouldGenerateBase64FromString3")]
    public void ShouldGenerateBase64FromString(string value)
    {
        #region Arrange & Act
        var base64Str = value.ToBase64();
        #endregion

        #region Assert
        Assert.True(IsBase64String(base64Str));
        #endregion
    }

    private bool IsBase64String(string base64)
    {
        if (string.IsNullOrEmpty(base64))
            return false;

        Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer, out _);
    }
}