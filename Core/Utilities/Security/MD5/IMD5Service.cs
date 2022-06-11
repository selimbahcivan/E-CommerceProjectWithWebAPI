using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.MD5
{
    public interface IMD5Service
    {
        /// <summary>
        /// Metni MD5'e dönüştür
        /// </summary>
        /// <param name="Text">string text</param>
        /// <returns>string text</returns>
        string ConvertTextToMD5(string Text);
        /// <summary>
        /// MD5'le şifreleme
        /// </summary>
        /// <param name="Text">string text</param>
        /// <returns>string text</returns>
        string Encrypt(string Text);
        /// <summary>
        /// Şifresini çöz (MD5)
        /// </summary>
        /// <param name="encryptedValue">string MD5</param>
        /// <returns>string text</returns>
        string Decrypt(string encryptedValue);

    }
}
