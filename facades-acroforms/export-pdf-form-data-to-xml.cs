using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_data.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document document = new Document(inputPdfPath))
        {
            // Enable automatic recalculation of form fields (no Recalculate method exists)
            document.Form.AutoRecalculate = true;

            // Export all form field values to JSON (ExportDataToXML does not exist)
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                document.Form.ExportToJson(jsonStream);
            }
        }

        Console.WriteLine($"Form data exported to JSON file: {outputJsonPath}");
    }
}