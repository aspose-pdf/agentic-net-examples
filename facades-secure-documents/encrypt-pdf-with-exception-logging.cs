using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Define the permissions you want to allow (combine with | if needed)
                var permissions = Permissions.PrintDocument;

                // Apply encryption using AES 256-bit algorithm
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine("Encryption succeeded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Encryption failed with an exception:");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
}
