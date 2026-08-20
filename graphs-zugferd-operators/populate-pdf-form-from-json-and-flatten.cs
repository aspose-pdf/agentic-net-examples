using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPdf = "filled_flattened.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Populate form fields from the JSON file
                doc.Form.ImportFromJson(jsonPath);

                // Flatten the form to make fields non‑editable
                doc.Form.Flatten();

                // Save the updated PDF
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