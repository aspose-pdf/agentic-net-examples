using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "protected.pdf";
        const string outputPath = "protected_edited.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF with the user password, then decrypt it.
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Decrypt the document (no parameters required).
            doc.Decrypt();

            // ---- Edit the PDF content (example: add a text fragment) ----
            // Pages are 1‑based indexed.
            Page firstPage = doc.Pages[1];

            TextFragment fragment = new TextFragment("Edited content");
            fragment.Position = new Position(100, 700); // X, Y coordinates.
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            firstPage.Paragraphs.Add(fragment);
            // -------------------------------------------------------------

            // Re‑encrypt the document using AES‑256 and desired permissions.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the modified and re‑encrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Decrypted, edited, and re‑encrypted PDF saved to '{outputPath}'.");
    }
}