using System.Security.Cryptography;
using System.Collections.Generic;
using System;

namespace MyVotingSystem.Engines{

    static public class AuthenticationCodeEngine{
        static private Dictionary<string, string> _emailCodes = new Dictionary<string, string>();
        static private int _codeLength = 10;

        static public string create(string email){
            string chars = "abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string code = RandomNumberGenerator.GetString(chars, _codeLength);
            _emailCodes.Add(email, code);
            return code;
        }

        static public bool verify(string email, string confirmationCode){
            if(_emailCodes.ContainsKey(email)){
                return _emailCodes[email]==confirmationCode;
            } else{
                return false;
            }
        }
    }
}