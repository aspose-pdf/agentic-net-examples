using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Generate a secure random token (32 bytes, Base64 encoded)
        string authToken;
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] tokenBytes = new byte[32];
            rng.GetBytes(tokenBytes);
            authToken = Convert.ToBase64String(tokenBytes);
        }

        // Load the PDF using the Facades Form class (fully qualified to avoid ambiguity)
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form())
        {
            pdfForm.BindPdf(inputPath);               // Initialize the facade with the source PDF
            Document doc = pdfForm.Document;          // Access the underlying Document object

            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Create a hidden text field named "AuthToken" on the first page
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            // Rectangle with zero size; field will not be visible
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField hiddenField = new TextBoxField(firstPage, rect)
            {
                PartialName = "AuthToken",
                Value = authToken
                // No need to set Flags; zero‑size rectangle makes the field invisible
            };

            // Add the field to the document's form
            doc.Form.Add(hiddenField);

            // Save the modified PDF via the Form facade
            pdfForm.Save(outputPath);
        }

        Console.WriteLine($"Hidden field \"AuthToken\" added and saved to '{outputPath}'.");
    }
}
