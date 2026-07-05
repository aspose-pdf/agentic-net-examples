using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Convert the JSON string to a UTF‑8 memory stream
        using (MemoryStream jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonData)))
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Import form field values from the JSON stream
                pdfDoc.Form.ImportFromJson(jsonStream);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}