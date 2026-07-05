using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_token.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Generate a secure random token (e.g., 32‑byte Base64 string)
        string authToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        // Load the PDF, add a hidden form field, and save
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Create a zero‑size TextBoxField (hidden by size) on the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField hiddenField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "AuthToken",
                Value = authToken
                // No need to set Flags; zero‑size rectangle makes it invisible
            };

            // Add the field to the document's form collection (page index 1)
            doc.Form.Add(hiddenField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden AuthToken field at '{outputPath}'.");
    }
}
