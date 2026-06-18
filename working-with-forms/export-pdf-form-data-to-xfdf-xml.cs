using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing the form.
        const string inputPdfPath = "input.pdf";

        // Desired path for the exported XML (XFDF) file.
        const string outputXmlPath = "formData.xml";

        // Verify that the input PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Export all form annotations (XFDF) to the specified XML file.
                // XFDF is an XML representation of form field values.
                pdfDocument.ExportAnnotationsToXfdf(outputXmlPath);
            }

            Console.WriteLine($"Form data successfully exported to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or export.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}