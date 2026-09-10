using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = "pdfs";

        // Directory where the XML files will be written
        const string outputDirectory = "xml_output";

        // Verify the input directory exists
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
            // Preserve the original file name, change only the extension to .xml
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(outputDirectory, baseName + ".xml");

            try
            {
                // Load the PDF document
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Use XmlSaveOptions to force XML output
                    XmlSaveOptions xmlOptions = new XmlSaveOptions();

                    // Save the document as XML
                    pdfDocument.Save(xmlPath, xmlOptions);
                }

                Console.WriteLine($"Converted: {pdfPath} → {xmlPath}");
            }
            catch (Exception ex)
            {
                // Report any errors but continue processing remaining files
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }
    }
}