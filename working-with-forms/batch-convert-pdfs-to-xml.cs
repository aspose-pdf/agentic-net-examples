using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDir = "input_pdfs";

        // Directory where XML files will be written
        const string outputDir = "output_xml";

        // Verify input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Preserve original file name, change extension to .xml
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(outputDir, baseName + ".xml");

            try
            {
                // Load PDF and automatically dispose it after use
                using (Document doc = new Document(pdfPath))
                {
                    // Use XmlSaveOptions because we are saving to a non‑PDF format
                    XmlSaveOptions xmlOptions = new XmlSaveOptions();

                    // Save as XML preserving the original base name
                    doc.Save(xmlPath, xmlOptions);
                }

                Console.WriteLine($"Converted: {pdfPath} → {xmlPath}");
            }
            catch (Exception ex)
            {
                // Log any conversion errors but continue processing other files
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }
    }
}