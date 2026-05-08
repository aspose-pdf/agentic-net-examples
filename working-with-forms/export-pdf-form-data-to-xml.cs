using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing a form.
        const string inputPdfPath  = "input.pdf";

        // Destination XML file that will hold the exported form data.
        const string outputXmlPath = "form_data.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export the entire PDF document model (including form fields and their values)
                // to an XML representation.
                pdfDoc.SaveXml(outputXmlPath);
            }

            Console.WriteLine($"Form data exported to XML file: {outputXmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}