using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Add a text fragment to the first page (pages are 1‑based)
                Page page = doc.Pages[1];
                TextFragment tf = new TextFragment("Confidential document");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8, 0.0, 0.0); // cross‑platform color
                tf.Position = new Position(100, 750); // place near top-left
                page.Paragraphs.Add(tf);

                // Encrypt the document with a user password (AES‑256)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
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