using System;
using System.IO;
using Aspose.Pdf;

class ExportAccessibilityData
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output XML file path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ExportAccessibilityData <input-pdf> <output-xml>");
            return;
        }

        string inputPdfPath = args[0];
        string outputXmlPath = args[1];

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize XmlSaveOptions – this will export the PDF's logical structure (accessibility data)
                XmlSaveOptions saveOptions = new XmlSaveOptions();

                // Save the accessibility data to the specified XML file
                pdfDocument.Save(outputXmlPath, saveOptions);
            }

            Console.WriteLine($"Accessibility data exported successfully to: {outputXmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}