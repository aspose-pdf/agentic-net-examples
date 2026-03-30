using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Decrypt the document so it can be edited
                doc.Decrypt();

                // Simple edit: add a text annotation on the first page
                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title = "Note",
                    Contents = "Edited after decryption",
                    Color = Aspose.Pdf.Color.Yellow,
                    Open = true,
                    Icon = TextIcon.Note
                };
                page.Annotations.Add(annotation);

                // Re‑encrypt the document with a strong algorithm
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted, edited, and re‑encrypted PDF saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}