using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "edited_encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the encrypted PDF using the user password.
        using (Document pdf = new Document(inputPath, userPassword))
        {
            // Add a text fragment to the first page.
            TextFragment tf = new TextFragment("Edited content added via Document API.")
            {
                Position = new Position(100, 700)
            };
            // Optional styling.
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Black;

            pdf.Pages[1].Paragraphs.Add(tf);

            // Re‑encrypt the PDF with desired permissions.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            pdf.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            pdf.Save(outputPath);
        }

        Console.WriteLine($"Edited and re‑encrypted PDF saved to '{outputPath}'.");
    }
}
