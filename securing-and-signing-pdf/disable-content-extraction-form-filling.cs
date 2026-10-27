using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "signed_privileges.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the signed PDF
            using (Document doc = new Document(inputPath))
            {
                // Allow printing and annotation editing, but disable content extraction and form filling
                Permissions allowed = Permissions.PrintDocument | Permissions.ModifyContent;

                // Apply encryption with the specified permissions
                doc.Encrypt(userPassword, ownerPassword, allowed, CryptoAlgorithm.AESx256);

                // Save the PDF with the new privileges
                doc.Save(outputPath);
            }

            Console.WriteLine($"Privileges set and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
