using System.Security.Cryptography;

namespace ScaleVoting.Extensions
{
    public static class DigitalSignature
    {
        private static readonly RSACryptoServiceProvider RsaAlg = new RSACryptoServiceProvider(2048);
        private static readonly RSAParameters PrivateKey = RsaAlg.ExportParameters(true);
        public static readonly RSAParameters OpenKey = RsaAlg.ExportParameters(false);

        public static byte[] GetSignatureFor(byte[] data)
        {
            return HashAndSignBytes(data);
        }

        private static byte[] HashAndSignBytes(byte[] dataToSign)
        {
            RsaAlg.ImportParameters(PrivateKey);

            return RsaAlg.SignData(dataToSign, new SHA256CryptoServiceProvider());
        }
    }
}