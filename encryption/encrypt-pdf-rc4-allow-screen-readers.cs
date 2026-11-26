using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "noPwdAllowScreen.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF (no password required)
        using (Document doc = new Document(inputPath))
        {
            // Set permissions: allow screen readers (ExtractContentWithDisabilities)
            Permissions perms = Permissions.ExtractContentWithDisabilities;

            // Encrypt with RC4x128, no user/owner passwords (empty strings)
            doc.Encrypt(string.Empty, string.Empty, perms, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved as '{outputPath}'.");
    }
}