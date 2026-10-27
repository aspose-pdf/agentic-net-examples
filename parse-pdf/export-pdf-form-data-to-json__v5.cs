using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "formdata.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export the form data directly to a JSON file stream
                using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
                {
                    pdfDoc.Form.ExportToJson(jsonStream);
                }
            }

            Console.WriteLine($"Form data successfully exported to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
