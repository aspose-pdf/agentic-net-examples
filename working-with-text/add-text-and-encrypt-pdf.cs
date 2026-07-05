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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Add a text fragment to the first page
                Page page = doc.Pages[1];
                TextFragment tf = new TextFragment("Confidential Document");
                tf.Position = new Position(100, 700);
                tf.TextState.FontSize = 24;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                page.Paragraphs.Add(tf);

                // Define permissions (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document with user and owner passwords using AES-256
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