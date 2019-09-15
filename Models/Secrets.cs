using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Models
{
    public class Secrets
    {
        private static string _JWTKey;
        private static string _ConnectionDB;
        public static string ConnectionDB
        {
            get
            {
                return _ConnectionDB;
            }
            set
            {
                _ConnectionDB = value;
            }
        }
        public static string JWTKey
        {
            get
            {
                return _JWTKey;
            }
            set
            {
                _JWTKey = value;
            }
        }
    }
}
