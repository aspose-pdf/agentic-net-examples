using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_filled.pdf";

        // JSON string containing form field values (field names must match PDF form field names)
        const string jsonData = @"{
            ""FirstName"": ""John"",
            ""LastName"": ""Doe"",
            ""Email"": ""john.doe@example.com""
        }";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Convert the JSON string to a UTF‑8 memory stream
            using (MemoryStream jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonData)))
            {
                // Import form field values from the JSON stream into the PDF form
                pdfDocument.Form.ImportFromJson(jsonStream);
            }

            // Save the modified PDF with populated form fields
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported successfully. Saved to '{outputPdfPath}'.");
    }
}