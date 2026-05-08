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
        const string outputPdfPath = "filled_output.pdf";

        // JSON string containing form field values (example format)
        string jsonData = @"{
            \""FirstName\"": \""John\"",
            \""LastName\"":  \""Doe\"",
            \""Age\"":        30,
            \""Subscribe\"": true
        }";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Convert the JSON string to a memory stream (UTF-8 encoding)
            using (MemoryStream jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                // Import form field values from the JSON stream into the PDF form
                // The method returns a collection of FieldSerializationResult which can be inspected if needed
                var importResults = pdfDocument.Form.ImportFromJson(jsonStream);

                // Optional: log import results – use ToString() because specific properties may vary between versions
                foreach (var result in importResults)
                {
                    Console.WriteLine(result.ToString());
                }
            }

            // Save the updated PDF (standard PDF output)
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and PDF saved to '{outputPdfPath}'.");
    }
}