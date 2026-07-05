using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing XML files to be converted
        const string inputDirectory = "XmlInputs";
        // Directory where generated PDFs will be saved
        const string outputDirectory = "PdfOutputs";

        // Ensure both input and output directories exist
        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(outputDirectory);

        // Get all XML files in the input directory
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine($"[INFO] No XML files found in '{inputDirectory}'. Nothing to process.");
            return;
        }

        // Process each XML file in the input directory
        foreach (string xmlFilePath in xmlFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(xmlFilePath);
            string pdfFilePath = Path.Combine(outputDirectory, baseName + ".pdf");

            Console.WriteLine($"[START] Processing XML file: {xmlFilePath}");

            try
            {
                // STEP 1: Create a new empty PDF document
                using (Document pdfDocument = new Document())
                {
                    Console.WriteLine("[STEP] Document instance created.");

                    // STEP 2: Load the XML content into the document using BindXml
                    pdfDocument.BindXml(xmlFilePath);
                    Console.WriteLine("[STEP] XML content bound to document.");

                    // STEP 3: Save the document as PDF
                    pdfDocument.Save(pdfFilePath);
                    Console.WriteLine($"[SUCCESS] PDF generated at: {pdfFilePath}");
                }
            }
            catch (PdfException pdfEx)
            {
                // Specific handling for Aspose.Pdf exceptions
                Console.Error.WriteLine($"[ERROR] PdfException while processing '{xmlFilePath}': {pdfEx.Message}");
            }
            catch (Exception ex)
            {
                // General exception handling
                Console.Error.WriteLine($"[ERROR] Unexpected error while processing '{xmlFilePath}': {ex.Message}");
            }

            Console.WriteLine($"[END] Finished processing '{xmlFilePath}'.\n");
        }
    }
}
