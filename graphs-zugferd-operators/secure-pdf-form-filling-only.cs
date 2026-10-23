using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF and apply security settings
            using (Document doc = new Document(inputPath))
            {
                // Allow only form filling; all other actions (editing, printing, extraction) are denied.
                // The FillForms permission flag is not available in older Aspose.Pdf versions, so we use its numeric value (0x100).
                Permissions perms = (Permissions)0x100; // FillForms permission
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the secured PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Secured PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
