using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;
using UnityEditor;
namespace CA.Data
{
    /// <summary>
    /// Firebase Auth 정보를 저장하는 클레스
    /// </summary>
    [CreateAssetMenu(fileName = "AuthData", menuName = "Scriptable Objects/AuthData")]
    public class AuthData : ScriptableObject
    {
        [SerializeField] private string _uid;
        [SerializeField] private string _email;
        [SerializeField] private string _password;


        public string Email { get { return _email; } set { _email = value; } }
        public string Uid {  get { return _uid; } set { _uid = value; } }

        /// <summary>
        /// sha256 password
        /// </summary>
        public string Password {
            get { return _password; }
            set {
                SHA256 sha256 = SHA256.Create();
                byte[] passwordByte = Encoding.UTF8.GetBytes(value);
                byte[] hash = sha256.ComputeHash(passwordByte);
                _password = BitConverter.ToString(hash).Replace("-","").ToLower();
            }
        }
        public void SaveAuthData() {
            PlayerPrefs.SetString("uid", _uid);
            PlayerPrefs.SetString("email", _email);
            PlayerPrefs.SetString("password", _password);
        }

        public void LoadAuthData() {
            if (PlayerPrefs.HasKey("uid")) {
                _uid = PlayerPrefs.GetString("uid");
                _email = PlayerPrefs.GetString("email");
                _password = PlayerPrefs.GetString("password");
            }
        }
    }
}
