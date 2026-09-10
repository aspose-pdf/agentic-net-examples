using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "protected.pdf";
        const string outputPath = "edited_encrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Decrypt the document so it can be edited
                doc.Decrypt();

                // Example edit: add a text fragment to the first page
                Page page = doc.Pages[1];
                TextFragment tf = new TextFragment("Edited after decryption");
                tf.Position = new Position(100, 700);
                page.Paragraphs.Add(tf);

                // Re‑encrypt the document with desired permissions
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the edited and re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully processed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}