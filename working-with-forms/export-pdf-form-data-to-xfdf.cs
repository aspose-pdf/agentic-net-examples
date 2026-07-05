using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Path to the source PDF
        const string outputXmlPath = "formData.xfdf";      // Desired output XML (XFDF) file

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Export all form annotations (including field values) to XFDF (XML) format
                pdfDocument.ExportAnnotationsToXfdf(outputXmlPath);
            }

            Console.WriteLine($"Form data successfully exported to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}