using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Add a simple text fragment to the first page
                Page page = doc.Pages[1];
                TextFragment tf = new TextFragment("Confidential");
                tf.Position = new Position(100, 700);               // Position on the page
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 24;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                page.Paragraphs.Add(tf);

                // Define permissions and encrypt the document
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