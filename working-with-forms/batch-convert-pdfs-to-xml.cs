using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDir = @"C:\PdfInput";
        // Output directory for XML form data files
        const string outputDir = @"C:\PdfOutput";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(outputDir, fileNameWithoutExt + ".xml");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Save as XML form data using explicit XmlSaveOptions
                    XmlSaveOptions xmlOpts = new XmlSaveOptions();
                    doc.Save(xmlPath, xmlOpts);
                }

                Console.WriteLine($"Converted: {Path.GetFileName(pdfPath)} → {Path.GetFileName(xmlPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}