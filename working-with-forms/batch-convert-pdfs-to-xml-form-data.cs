using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // XmlSaveOptions is also in this namespace

class BatchPdfToXmlConverter
{
    static void Main()
    {
        // Directory containing PDF files
        const string inputDirectory = @"C:\PdfInput";

        // Verify the directory exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Process each PDF file in the directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Determine output XML file path (same name, .xml extension)
                string xmlPath = Path.ChangeExtension(pdfPath, ".xml");

                // Load PDF and save as XML using XmlSaveOptions
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Initialize XmlSaveOptions (default constructor)
                    XmlSaveOptions xmlOptions = new XmlSaveOptions();

                    // Save the document as XML form data
                    pdfDocument.Save(xmlPath, xmlOptions);
                }

                Console.WriteLine($"Converted: {Path.GetFileName(pdfPath)} → {Path.GetFileName(xmlPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}