using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "encrypted.pdf";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPdf);

            // Combine privilege settings: allow printing and editing (modify content)
            Permissions permissions = Permissions.PrintDocument | Permissions.ModifyContent;

            // Encrypt using RC4‑128 algorithm
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(outputPdf);

            Console.WriteLine($"PDF encrypted successfully and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
