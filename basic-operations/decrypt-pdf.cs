using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string outputPath = "decrypted.pdf";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(encryptedPath, userPassword))
            {
                doc.Decrypt();
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}