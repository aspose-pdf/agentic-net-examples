using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for TextBoxField

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Generate a secure token to be stored in the hidden field
        string authToken = GenerateSecureToken();

        using (Document doc = new Document(inputPath))
        {
            // Define a zero‑size rectangle (field placed off‑page because it is hidden)
            // The rectangle must be on a page; we use the first page here.
            Rectangle rect = new Rectangle(0, 0, 0, 0);

            // Create a TextBox field using the page‑based constructor
            TextBoxField authField = new TextBoxField(doc.Pages[1], rect);
            authField.PartialName = "AuthToken"; // set the field name
            authField.Value = authToken;
            // The field is effectively hidden by the zero‑size rectangle; no Flag enum is required.

            // Add the field to the document's form on page 1
            doc.Form.Add(authField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Added hidden AuthToken field and saved to '{outputPath}'.");
    }

    private static string GenerateSecureToken()
    {
        byte[] tokenBytes = new byte[32];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(tokenBytes);
        }
        return Convert.ToBase64String(tokenBytes);
    }
}
