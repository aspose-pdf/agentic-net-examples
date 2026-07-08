using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF with form fields
        const string jsonPath  = "data.json";   // JSON array containing field values
        const string outputPath = "output.pdf"; // PDF after bulk import

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Open the JSON file as a stream and import all form fields at once
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                {
                    doc.Form.ImportFromJson(jsonStream);
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}