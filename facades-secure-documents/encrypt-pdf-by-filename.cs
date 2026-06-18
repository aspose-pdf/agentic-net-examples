using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a self‑contained sample PDF
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("sample.pdf");
        }

        // Define output files – the name determines the encryption algorithm
        string[] outputFiles = new string[] { "aes128.pdf", "aes256.pdf", "rc4-128.pdf" };
        foreach (string outFile in outputFiles)
        {
            using (Document doc = new Document("sample.pdf"))
            {
                // Common permissions for the example
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Choose CryptoAlgorithm based on the file name
                CryptoAlgorithm algo;
                if (outFile.Contains("aes128"))
                {
                    algo = CryptoAlgorithm.AESx128;
                }
                else if (outFile.Contains("aes256"))
                {
                    algo = CryptoAlgorithm.AESx256;
                }
                else // rc4‑128
                {
                    algo = CryptoAlgorithm.RC4x128;
                }

                // Apply encryption and save the protected PDF
                doc.Encrypt("userPassword", "ownerPassword", perms, algo);
                doc.Save(outFile);
                Console.WriteLine($"Encrypted '{outFile}' using {algo}.");
            }
        }
    }
}