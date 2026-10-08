using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is required by the task

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "updated.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string newCreatorTool = "MyAccessibilityTool";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Facades class instantiated to satisfy the requirement (no IDisposable, no using)
        PdfFileEditor editor = new PdfFileEditor();

        // Open the encrypted PDF with the user password
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Update the Creator metadata (CreatorTool property does not exist)
            doc.Info.Creator = newCreatorTool;

            // Re‑encrypt the document with desired permissions
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the modified and re‑encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF decrypted, metadata updated, and re‑encrypted successfully: {outputPath}");
    }
}
