using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing input XML files
        const string inputDir = "XmlInputs";
        // Directory where generated PDFs will be saved
        const string outputDir = "PdfOutputs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Get all XML files in the input directory
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        // Process each XML file individually
        foreach (string xmlPath in xmlFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDir, baseName + ".pdf");

            Console.WriteLine($"--- Processing '{xmlPath}' ---");

            try
            {
                // Load the XML file as a PDF document
                XmlLoadOptions loadOptions = new XmlLoadOptions();
                using (Document doc = new Document(xmlPath, loadOptions))
                {
                    Console.WriteLine($"Loaded XML successfully. Page count: {doc.Pages.Count}");

                    // (Optional) Insert additional PDF generation steps here
                    // e.g., add headers, footers, watermarks, etc.

                    // Save the generated PDF
                    doc.Save(pdfPath);
                    Console.WriteLine($"PDF saved to '{pdfPath}'.");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }

            Console.WriteLine($"--- Finished '{xmlPath}' ---\n");
        }

        Console.WriteLine("All files have been processed.");
    }
}