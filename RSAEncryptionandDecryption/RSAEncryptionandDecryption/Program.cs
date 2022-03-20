using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace RSAEncryptionandDecryption
{
    class Program
    {

        public class RSAEncryption
        {
            public static int valueLength = 0;

            public int inputLength(string inputLen)
            {
                int value = 0;
                for (int i = 0; i < inputLen.Length;i++)
                {
                    value = i;
                }
                Console.WriteLine("Length Text: " + value);
                return value;
            }

            

            private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(RSAEncryption.valueLength);
            
            private RSAParameters privateKey;
            private RSAParameters publicKey;


            public RSAEncryption()
            {
                privateKey = csp.ExportParameters(true);
                publicKey = csp.ExportParameters(false);
            }


            //Method to GetThePublicKey
            public string GetPublicKey()
            {
                //sw = stringwriter
                var sw = new StringWriter();
                //xs = XMLSerializer
                var xs = new XmlSerializer(typeof(RSAParameters));
                xs.Serialize(sw,publicKey);

                return sw.ToString();


            }

            public string Encrypt(string PlainText)
            {
                csp = new RSACryptoServiceProvider();
                csp.ImportParameters(publicKey);

                var data = Encoding.Unicode.GetBytes(PlainText);

                var cypher = csp.Encrypt(data,false);

                return Convert.ToBase64String(cypher);
            }


            public string Decrypt(string cypherText)
            {

                var dataBytes = Convert.FromBase64String(cypherText);

                csp.ImportParameters(privateKey);

                var plainText = csp.Decrypt(dataBytes,false);

                return Encoding.Unicode.GetString(plainText);
            }

            
        }

        public class MainFunctionRSA
        {
            public void mainFunction()
            {
                try
                {

                    while (true)
                    {
                        Console.Write("---------------------------------------------------------------------------------------------------------------\n"
                                + "Enter the following and choose the correct option below\n"
                                + "1. Create a new message\n"
                                + "2. Exit\n"
                                + "---------------------------------------------------------------------------------------------------------------\n");
                        int chooseNumber = Convert.ToInt32(Console.ReadLine());
                        if (chooseNumber == 1)
                        {
                            RSAEncryption rsa = new RSAEncryption();
                            string cypher = string.Empty;

                            Console.WriteLine("Enter your message:");
                            var text = Console.ReadLine();
                            int newvalue = rsa.inputLength(text);

                            RSAEncryption.valueLength = newvalue;

                            Console.WriteLine($"Public Key: \n{rsa.GetPublicKey()} \n");



                            if (!string.IsNullOrEmpty(text))
                            {
                                cypher = rsa.Encrypt(text);

                                Console.WriteLine($"Encrypted Message: \n{cypher}\n");
                            }


                            Console.WriteLine("Press any key to Decrypt Message");
                            Console.ReadLine();

                            var plaintext = rsa.Decrypt(cypher);

                            Console.WriteLine($"Decrypt Message: {plaintext}");
                        }
                        else if (chooseNumber == 2)
                        {
                            Console.WriteLine("Have a nice day!!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Correct format: Example: 1 or 2");
                        }
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.Message + " Contact the developer or you can private message me on my FB: www.faceboo.com/1c4ctub");
                }

            }
        }
        static void Main(string[] args)
        {
            MainFunctionRSA apps = new MainFunctionRSA();

            apps.mainFunction();


        }
        
    }
}
