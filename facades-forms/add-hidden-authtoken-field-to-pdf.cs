using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // required for TextBoxField and other form classes

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Generate a secure random token (256‑bit) and encode it as Base64
        string authToken = GenerateSecureToken(32); // 32 bytes = 256 bits

        // Use the Facades Form class to bind the PDF, modify it, and save it
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form())
        {
            // Load the existing PDF
            pdfForm.BindPdf(inputPdf);

            // Access the underlying Document object
            Document doc = pdfForm.Document;

            // Create a hidden text field on the first page.
            // A zero‑size rectangle makes the field invisible; no need for FieldFlags.
            var hiddenField = new TextBoxField(
                doc.Pages[1],
                new Rectangle(0, 0, 0, 0) // zero‑size rectangle
            )
            {
                PartialName = "AuthToken",
                Value = authToken
            };

            // Add the field to the document's AcroForm
            doc.Form.Add(hiddenField);

            // Save the modified PDF via the Facades API
            pdfForm.Save(outputPdf);
        }

        Console.WriteLine($"Hidden field \"AuthToken\" added and saved to '{outputPdf}'.");
    }

    // Helper: generate a cryptographically strong random token and return Base64 string
    private static string GenerateSecureToken(int byteLength)
    {
        byte[] tokenBytes = new byte[byteLength];
        RandomNumberGenerator.Fill(tokenBytes);
        return Convert.ToBase64String(tokenBytes);
    }
}
