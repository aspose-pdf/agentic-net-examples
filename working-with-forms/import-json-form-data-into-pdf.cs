using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the input PDF and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_filled.pdf";

        // JSON string containing form field values.
        // The JSON keys must match the full names of the form fields.
        string jsonData = @"{
            ""FirstName"": ""John"",
            ""LastName"": ""Doe"",
            ""Email"": ""john.doe@example.com"",
            ""AgreeTerms"": true
        }";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document, import JSON data into its form fields, and save the result.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Convert the JSON string to a UTF‑8 memory stream.
            using (MemoryStream jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                // Import the form field values from the JSON stream.
                // This matches fields by their full names.
                pdfDoc.Form.ImportFromJson(jsonStream);
            }

            // Save the updated PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}