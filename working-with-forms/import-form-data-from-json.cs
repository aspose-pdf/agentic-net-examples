using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "filled.pdf";

        // JSON string containing form field names and their values
        string jsonData = @"{
            ""FirstName"": ""John"",
            ""LastName"": ""Doe"",
            ""Age"": ""30""
        }";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Convert the JSON string to a UTF‑8 memory stream
            using (MemoryStream jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonData)))
            {
                // Import form field values from the JSON stream
                var importResults = doc.Form.ImportFromJson(jsonStream);

                // Optionally report how many fields were processed
                int processed = 0;
                foreach (var _ in importResults)
                    processed++;

                Console.WriteLine($"Imported data for {processed} form fields.");
            }

            // Save the updated PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}