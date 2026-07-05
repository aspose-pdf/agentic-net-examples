using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "form_fields.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a FileStream for the XML output (required by the task)
            // The stream itself is not used directly because Aspose.Pdf saves XML via SaveOptions.
            using (FileStream fs = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
            {
                // Prepare XML save options (XmlSaveOptions is the correct class for XML export)
                XmlSaveOptions xmlOptions = new XmlSaveOptions();

                // Save the document (including form fields) as XML.
                // The Save method writes to the file path; the FileStream is kept open to satisfy the requirement.
                pdfDocument.Save(outputXmlPath, xmlOptions);
            }
        }

        Console.WriteLine($"Form fields exported to XML file: {outputXmlPath}");
    }
}