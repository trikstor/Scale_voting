using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using ScaleVoting.Domains;
using ScaleVoting.Extensions;

namespace ScaleVoting.BlockChainClient.BlockChainCore
{
    [Serializable]
    public struct Transaction
    {
        public string UserHash;
        public byte[] Data;
        public byte[] Signature;
        public RSAParameters Key;
        public bool Valid => Validate();

        private bool Validate()
        {
            try
            {
                var crypt = new RSACryptoServiceProvider();
                crypt.ImportParameters(DigitalSignature.OpenKey);
                return crypt.VerifyData(Data, new SHA256CryptoServiceProvider(), Signature);
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        public Transaction(Vote vote)
        {
            Data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(vote));
            UserHash = vote.UserHash;
            Key = DigitalSignature.OpenKey;
            Signature = DigitalSignature.GetSignatureFor(Data);
        }

        public Vote ToVote()
        {
            var vote = JsonConvert.DeserializeObject<Vote>(Encoding.UTF8.GetString(Data));
            vote.UserHash = UserHash;
            return vote;
        }
    }
}