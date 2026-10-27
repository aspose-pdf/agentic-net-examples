using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "encrypted_view_only.pdf";
        const string ownerPassword  = "ownerSecret";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Permissions that allow viewing/printing but prevent editing
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Empty user password => no password required to open/view the PDF
                // Owner password protects the permissions and is required for editing
                doc.Encrypt(
                    userPassword: "",          // no password for opening
                    ownerPassword: ownerPassword,
                    permissions: perms,
                    cryptoAlgorithm: CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted successfully. Viewable without password, editing requires the owner password.\nSaved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}