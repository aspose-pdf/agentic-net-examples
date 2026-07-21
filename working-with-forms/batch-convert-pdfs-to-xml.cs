using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // XmlSaveOptions is also in this namespace

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDir = @"C:\InputPdfs";
        // Output directory where XML files will be written
        const string outputDir = @"C:\OutputXml";

        // Validate input directory
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to convert.");
            return;
        }

        // Prepare XML save options (required for non‑PDF output)
        XmlSaveOptions xmlOptions = new XmlSaveOptions();

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Build the output XML file path, preserving the original base name
                    string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
                    string xmlPath = Path.Combine(outputDir, xmlFileName);

                    // Save the document as XML using the explicit XmlSaveOptions
                    pdfDoc.Save(xmlPath, xmlOptions);

                    Console.WriteLine($"Converted: {Path.GetFileName(pdfPath)} → {xmlFileName}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}