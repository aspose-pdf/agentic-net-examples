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

        // Passwords and permissions for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Add simple text to the first page
                Page firstPage = doc.Pages[1]; // 1‑based indexing
                TextFragment tf = new TextFragment("Confidential – Do not distribute");
                tf.Position = new Position(100, 700); // place near top-left
                tf.TextState.FontSize = 14;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Color.Red;
                firstPage.Paragraphs.Add(tf);

                // Encrypt the document with a user password
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

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