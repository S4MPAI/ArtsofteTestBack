using Logic.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Logic.Services;

public class SHA256Hasher : IHasher
{
    public string Create(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);

        return Convert.ToHexString(SHA256.HashData(inputBytes));
    }
}
