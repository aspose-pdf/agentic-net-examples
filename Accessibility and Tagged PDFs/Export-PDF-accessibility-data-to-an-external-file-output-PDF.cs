using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for XmlSaveOptions if needed

class ExportAccessibility
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string xmlPath  = "accessibility.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Export all annotations to XFDF (accessibility annotations)
                doc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

                // Export the tagged structure (accessibility data) to XML
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                doc.Save(xmlPath, xmlOptions);
                Console.WriteLine($"Accessibility data exported to '{xmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}