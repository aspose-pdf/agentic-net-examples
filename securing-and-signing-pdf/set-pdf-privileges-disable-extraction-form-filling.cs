using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_input.pdf";   // existing signed PDF
        const string outputPath = "privileged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (already signed). We only need to apply permission restrictions.
        using (Document doc = new Document(inputPath))
        {
            // ---------------------------------------------------------------
            // 1. Restrict permissions: allow only printing, disable content
            //    extraction and form filling.
            // ---------------------------------------------------------------
            Permissions allowedPermissions = Permissions.PrintDocument; // only printing allowed
            doc.Encrypt(
                userPassword: string.Empty,
                ownerPassword: string.Empty,
                permissions: allowedPermissions,
                cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // ---------------------------------------------------------------
            // 2. Save the updated PDF.
            // ---------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with restricted privileges to '{outputPath}'.");
    }
}
