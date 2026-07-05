using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonFile = "data.json";
        const string outputPdf = "output_flattened.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(jsonFile))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonFile}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Populate form fields from the JSON source
                doc.Form.ImportFromJson(jsonFile);

                // Flatten the form so fields become static content and cannot be edited
                doc.Form.Flatten();

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with populated and flattened form saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}