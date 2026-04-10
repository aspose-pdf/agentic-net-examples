using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed_input.pdf";   // existing signed PDF
        const string outputPath = "privileged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define passwords (can be empty strings if no password is required)
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Permissions: allow printing only; do NOT include ExtractContent or FillForm
        Permissions perms = Permissions.PrintDocument;

        try
        {
            // Load the signed PDF
            using (Document doc = new Document(inputPath))
            {
                // Apply encryption with the desired permissions
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the result; the document now has privileges that disable
                // content extraction and form filling
                doc.Save(outputPath);
            }

            Console.WriteLine($"Privileged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}