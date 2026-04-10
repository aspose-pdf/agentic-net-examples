using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output XML file that will contain the exported form fields (document model)
        const string outputXmlPath = "form_fields.xml";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Ensure the document actually contains a form
            if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any form fields to export.");
            }

            // Export the document (including its form fields) to XML.
            // Document.SaveXml writes the entire PDF structure to an XML file.
            pdfDocument.SaveXml(outputXmlPath);
        }

        // Optionally, confirm that the XML file was created
        if (File.Exists(outputXmlPath))
        {
            Console.WriteLine($"Form fields exported successfully to '{outputXmlPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to create the XML output file.");
        }
    }
}