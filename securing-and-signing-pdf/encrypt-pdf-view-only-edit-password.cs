using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_view_only.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Permissions that allow viewing/printing but prevent editing
        Permissions perms = Permissions.PrintDocument; // allow printing, no modify permissions

        // Empty user password => no password required to open/view
        // Owner password protects editing operations
        const string userPassword  = "";          // view without password
        const string ownerPassword = "EditOnly123";

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Encrypt the document: viewable without password, editing requires owner password
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}