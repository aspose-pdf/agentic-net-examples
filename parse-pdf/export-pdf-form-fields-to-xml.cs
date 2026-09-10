using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output XML file that will contain the document (including form fields) representation
        const string outputXmlPath = "form_fields.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Save the entire document model as XML.
                // This XML includes the form field definitions and their current values.
                pdfDocument.SaveXml(outputXmlPath);
            }

            Console.WriteLine($"Form fields exported to XML: '{outputXmlPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}