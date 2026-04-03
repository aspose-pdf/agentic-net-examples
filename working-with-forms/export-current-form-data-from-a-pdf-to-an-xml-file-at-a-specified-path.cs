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
        const string outputXmlPath = "formData.xfdf";

        // Verify that the input PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create & load).
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Export all form annotations (field names and values) to an XFDF file.
                // This method writes the XML representation directly to the specified path.
                pdfDocument.ExportAnnotationsToXfdf(outputXmlPath);
            }

            Console.WriteLine($"Form data successfully exported to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or exporting.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}