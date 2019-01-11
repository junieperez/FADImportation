
namespace FAD_Importation.CLASSES
{
    using System.Security.Cryptography;
    using System.Text;

    public class MD5Sample
    {
        public string _source { get; set; }
            public string GetMd5Hash()
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    // Convert the input string to a byte array and compute the hash.
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(_source));

                    // Create a new Stringbuilder to collect the bytes
                    // and create a string.
                    StringBuilder sBuilder = new StringBuilder();

                    // Loop through each byte of the hashed data 
                    // and format each one as a hexadecimal string.
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    // Return the hexadecimal string.
                    return sBuilder.ToString();
                }
            }
        }
    }

    // This code example produces the following output:
    //
    // The MD5 hash of Hello World! is: ed076287532e86365e841e92bfc50d8c.
    // Verifying the hash...
    // The hashes are the same.

