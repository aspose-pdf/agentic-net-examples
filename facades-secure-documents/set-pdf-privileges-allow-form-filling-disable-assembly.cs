using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDoc = new Document(inputPath);

            // Define the permissions: allow form filling, all other privileges (including document assembly) are omitted -> disabled.
            // Permissions enum lives in Aspose.Pdf.Security and contains FillForm and DocumentAssembly among others.
            Permissions perms = Permissions.FillForm; // only FillForm is granted

            // Apply the permissions. An empty user password means the file can be opened without a password,
            // but the owner password protects the permission settings.
            string userPassword = string.Empty; // no password required to open
            string ownerPassword = "ownerPassword123"; // any owner password you prefer
            pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

            // Save the modified PDF.
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Privileges set. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
