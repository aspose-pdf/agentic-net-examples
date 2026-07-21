using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_token.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Generate a secure random token (e.g., 32 bytes, Base64 encoded)
        string authToken = GenerateSecureToken(32);

        // Load the PDF, add a hidden form field, and save
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page (required for field placement)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a hidden text box field on the first page.
            // The rectangle is zero‑sized; a zero‑size field is not visible in the PDF viewer.
            TextBoxField hiddenField = new TextBoxField(doc.Pages[1], new Rectangle(0, 0, 0, 0))
            {
                PartialName = "AuthToken",
                Value = authToken
                // No need to set a flag; the zero‑size rectangle makes the field effectively hidden.
            };

            // Add the field to the document's form collection
            doc.Form.Add(hiddenField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden AuthToken field added. Saved to '{outputPath}'.");
    }

    // Generates a cryptographically strong random token and returns it as a Base64 string.
    private static string GenerateSecureToken(int byteLength)
    {
        byte[] tokenBytes = new byte[byteLength];
        RandomNumberGenerator.Fill(tokenBytes);
        return Convert.ToBase64String(tokenBytes);
    }
}
