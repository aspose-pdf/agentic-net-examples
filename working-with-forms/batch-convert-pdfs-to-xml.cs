using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and XmlSaveOptions

class Program
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = "pdfs";

        // Directory where the resulting XML files will be placed
        const string outputDirectory = "xml_output";

        // Verify that the input directory exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Preserve the original file name (without extension) for the XML output
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(outputDirectory, baseName + ".xml");

            try
            {
                // Load the PDF document (lifecycle: using ensures deterministic disposal)
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Initialize XmlSaveOptions (required for XML export)
                    XmlSaveOptions xmlOptions = new XmlSaveOptions();

                    // Save the document as XML, preserving the original base name
                    pdfDocument.Save(xmlPath, xmlOptions);
                }

                Console.WriteLine($"Converted: '{pdfPath}' → '{xmlPath}'");
            }
            catch (Exception ex)
            {
                // Report any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}