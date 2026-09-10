using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, XmlSaveOptions)

class BatchFormExport
{
    static void Main()
    {
        // Determine base directory (the folder where the executable runs)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Build input and output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "PdfInputs");
        string outputFolder = Path.Combine(baseDir, "PdfXmlOutputs");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Validate the input directory – if it does not exist, fall back to the current working directory
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' not found. Using current directory as fallback.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Batch export aborted.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build the corresponding XML file name
                string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
                string xmlPath = Path.Combine(outputFolder, xmlFileName);

                // Load the PDF and export its form data (and full structure) to XML
                using (Document doc = new Document(pdfPath))
                {
                    // Save as XML using explicit XmlSaveOptions (required for non‑PDF formats)
                    doc.Save(xmlPath, new XmlSaveOptions());
                }

                Console.WriteLine($"Exported '{pdfPath}' → '{xmlPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch export completed.");
    }
}
