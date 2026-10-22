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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Generate a cryptographically‑secure token
        string token = GenerateSecureToken();

        // Load the source PDF
        Document doc = new Document(inputPath);

        // Create a hidden text box field named "AuthToken"
        // Zero‑size rectangle makes the field invisible in the viewer
        var rect = new Rectangle(0, 0, 0, 0);
        TextBoxField authField = new TextBoxField(doc.Pages[1], rect)
        {
            PartialName = "AuthToken",
            Value = token
        };

        // Add the field to the form (page numbers are 1‑based)
        doc.Form.Add(authField, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Hidden AuthToken field added and saved to '{outputPath}'.");
    }

    static string GenerateSecureToken()
    {
        // 256‑bit random token
        byte[] data = new byte[32];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(data);
        }
        return Convert.ToBase64String(data);
    }
}
