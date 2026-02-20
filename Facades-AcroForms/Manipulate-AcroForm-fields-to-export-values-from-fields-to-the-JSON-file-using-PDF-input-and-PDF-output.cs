using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormFieldsToJson
{
    static void Main(string[] args)
    {
        // Input PDF, output PDF (unchanged) and JSON file paths
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";
        string outputJsonPath = "fields.json";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Initialize the Form facade and bind the PDF
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);

                // Export all form field values to a JSON file
                using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
                {
                    // The second parameter indicates whether to include empty fields (false = exclude)
                    form.ExportJson(jsonStream, false);
                }

                // Save (or copy) the PDF to the output location
                // The Document property gives access to the underlying Aspose.Pdf.Document
                var pdfDocument = form.Document;
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine("Form fields exported to JSON and PDF saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}