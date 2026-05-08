using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonData = "data.json";
        const string outputPdf = "filled_flattened.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(jsonData))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonData}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Populate form fields from the JSON file
                doc.Form.ImportFromJson(jsonData);

                // Flatten the form to make fields non‑editable
                doc.Form.Flatten();

                // Save the resulting PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Form filled and flattened PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}