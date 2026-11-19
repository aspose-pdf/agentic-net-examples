using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, XmlLoadOptions)

class XmlToPdfBatch
{
    static void Main()
    {
        // Input folder containing XML files
        const string inputFolder = @"C:\InputXml";
        // Output folder for generated PDFs
        const string outputFolder = @"C:\OutputPdf";

        // Validate input folder
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all XML files in the input folder (non‑recursive)
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml", SearchOption.TopDirectoryOnly);

        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load the XML file as a PDF document using XmlLoadOptions
                XmlLoadOptions loadOptions = new XmlLoadOptions();
                using (Document pdfDoc = new Document(xmlPath, loadOptions))
                {
                    // Determine output PDF path (same file name, .pdf extension)
                    string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                    string pdfPath = Path.Combine(outputFolder, pdfFileName);

                    // Save the document as PDF (default format)
                    pdfDoc.Save(pdfPath);
                }

                Console.WriteLine($"Converted: {Path.GetFileName(xmlPath)} → {Path.GetFileNameWithoutExtension(xmlPath)}.pdf");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
