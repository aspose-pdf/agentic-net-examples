using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // source PDF with form fields
        const string xmlPath   = "formData.xml";       // destination XML file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Export the current form data (and other interactive elements) to XML
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                doc.Save(xmlPath, xmlOptions);
            }

            Console.WriteLine($"Form data exported to XML: {xmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}