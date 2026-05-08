using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add text, encrypt, and save.
        using (Document doc = new Document(inputPath))
        {
            // Add a simple text fragment to the first page.
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            TextFragment tf = new TextFragment("Confidential Document");
            tf.Position = new Position(100, 700); // place near top-left
            tf.TextState.FontSize = 24;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Red;
            firstPage.Paragraphs.Add(tf);

            // Define permissions (e.g., allow printing, disallow content extraction).
            Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent;

            // Encrypt with AES‑256 algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}