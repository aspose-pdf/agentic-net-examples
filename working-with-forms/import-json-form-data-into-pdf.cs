using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // JSON string containing form field names and values
        const string jsonString = @"{
            ""FirstName"": ""John"",
            ""LastName"": ""Doe"",
            ""Email"": ""john.doe@example.com""
        }";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Convert the JSON string to a memory stream (UTF-8 encoding)
        using (MemoryStream jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString)))
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Import form fields from the JSON stream into the PDF form
                pdfDocument.Form.ImportFromJson(jsonStream);

                // Save the updated PDF (PDF format, no extra SaveOptions needed)
                pdfDocument.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}