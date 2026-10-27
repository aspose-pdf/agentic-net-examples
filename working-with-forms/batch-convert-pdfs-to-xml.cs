using System;
using System.IO;
using Aspose.Pdf;               // Document, XmlSaveOptions are in this namespace

class BatchPdfToXmlConverter
{
    static void Main()
    {
        // Input directory containing PDFs
        const string inputDir  = @"C:\PdfInput";
        // Output directory for XML files (will be created if it does not exist)
        const string outputDir = @"C:\XmlOutput";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            try
            {
                // Preserve original file name, change extension to .xml
                string fileName   = Path.GetFileNameWithoutExtension(pdfPath);
                string xmlPath    = Path.Combine(outputDir, fileName + ".xml");

                // Load PDF and save as XML using XmlSaveOptions (required to get XML output)
                using (Document doc = new Document(pdfPath))
                {
                    XmlSaveOptions xmlOptions = new XmlSaveOptions();
                    doc.Save(xmlPath, xmlOptions);
                }

                Console.WriteLine($"Converted: {pdfPath} -> {xmlPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}