using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Directories containing source PDFs, corresponding XML data, and output PDFs
        const string pdfDirectory = "pdfs";
        const string xmlDirectory = "xmls";
        const string outputDirectory = "output";

        // Verify that the required input directories exist; create output directory if needed
        if (!Directory.Exists(pdfDirectory))
        {
            Console.Error.WriteLine($"PDF source directory '{pdfDirectory}' does not exist. No files to process.");
            return;
        }
        if (!Directory.Exists(xmlDirectory))
        {
            Console.Error.WriteLine($"XML source directory '{xmlDirectory}' does not exist. No files to process.");
            return;
        }
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the source directory
        foreach (string pdfPath in Directory.GetFiles(pdfDirectory, "*.pdf"))
        {
            // Derive the base file name (without extension) to locate the matching XML file
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(xmlDirectory, baseName + ".xml");

            // Skip if the matching XML file does not exist
            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"XML file not found for PDF '{pdfPath}'. Skipping.");
                continue;
            }

            // Define the output PDF path
            string outputPath = Path.Combine(outputDirectory, baseName + "_filled.pdf");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Bind the XML data to the PDF form fields
                    pdfDocument.BindXml(xmlPath);

                    // Save the updated PDF; Save(string) writes a PDF regardless of extension
                    pdfDocument.Save(outputPath);
                }

                Console.WriteLine($"Successfully processed '{baseName}'. Output saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }
    }
}
