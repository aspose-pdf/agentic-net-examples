using System;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // If the input file does not exist, create a simple PDF so the example can run.
        if (!System.IO.File.Exists(inputPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Load the source PDF.
        using (var doc = new Document(inputPath))
        {
            // Define the permissions: allow printing, disallow content extraction (copying).
            Permissions permissions = Permissions.PrintDocument;

            // Owner password can be any random string; user password left empty (no opening password).
            string userPassword = string.Empty;
            string ownerPassword = Guid.NewGuid().ToString("N");

            // Apply the security settings.
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx128);

            // Save the protected PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Created '{outputPath}' with printing enabled and copying disabled.");
    }
}
