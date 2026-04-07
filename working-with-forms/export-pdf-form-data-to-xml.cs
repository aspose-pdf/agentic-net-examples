using System;
using System.IO;
using Aspose.Pdf;

class ExportFormDataToXml
{
    static void Main()
    {
        // Input PDF file containing the form.
        const string pdfPath = "input.pdf";

        // Desired output XML file path.
        const string xmlPath = "formData.xml";

        // Verify the input file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF document, export its form data (and overall structure) to XML.
        using (Document doc = new Document(pdfPath))
        {
            // Save the document model, which includes form fields, to an XML file.
            doc.SaveXml(xmlPath);
        }

        Console.WriteLine($"Form data exported to XML at '{xmlPath}'.");
    }
}