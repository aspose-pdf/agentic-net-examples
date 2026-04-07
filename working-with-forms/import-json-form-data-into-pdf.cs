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
        string jsonData = @"{
            ""FirstName"": ""John"",
            ""LastName"": ""Doe"",
            ""Email"": ""john.doe@example.com""
        }";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Convert the JSON string to a memory stream (UTF‑8 encoded)
        using (MemoryStream jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonData)))
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPdfPath))
            {
                // Import form fields from the JSON stream into the PDF form
                doc.Form.ImportFromJson(jsonStream);

                // Save the updated PDF (lifecycle rule: save inside the using block)
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}