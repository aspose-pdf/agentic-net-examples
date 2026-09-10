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

        // Generate a secure random token (256‑bit, Base64 encoded)
        string authToken;
        {
            byte[] tokenBytes = new byte[32]; // 256 bits
            RandomNumberGenerator.Fill(tokenBytes);
            authToken = Convert.ToBase64String(tokenBytes);
        }

        // Open the PDF, add a hidden field, and save
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("PDF has no pages.");
                return;
            }

            // Zero‑size rectangle makes the field invisible
            Rectangle rect = new Rectangle(0, 0, 0, 0);

            // Create the hidden text box field
            TextBoxField hiddenField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "AuthToken",
                Value = authToken
            };

            // Add the field to the form (the field itself is an annotation)
            doc.Form.Add(hiddenField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden AuthToken field added and saved to '{outputPath}'.");
    }
}
