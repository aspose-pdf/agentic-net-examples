using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // included as per task requirement

class Program
{
    static void Main()
    {
        const string inputPath      = "protected.pdf";
        const string outputPath     = "edited_encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF using the user password, then remove encryption
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Decrypt the document (no parameters required)
            doc.Decrypt();

            // ----- Edit the PDF content -----
            // Example: add a simple text paragraph on the first page
            Page page = doc.Pages[1];
            TextFragment fragment = new TextFragment("Edited content added after decryption.");
            fragment.TextState.FontSize = 12;
            fragment.Position = new Position(100, 700); // Aspose.Pdf.Text.Position
            page.Paragraphs.Add(fragment);

            // ----- Re‑encrypt the PDF -----
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the edited and re‑encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Edited and encrypted PDF saved to '{outputPath}'.");
    }
}