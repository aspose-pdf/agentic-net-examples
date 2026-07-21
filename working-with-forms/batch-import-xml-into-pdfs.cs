using System;
using System.IO;
using Aspose.Pdf;

class BatchXmlImporter
{
    static void Main()
    {
        // Directory containing the source PDFs and their matching XML files
        const string sourceDir = @"C:\InputFiles";
        // Directory where the resulting PDFs will be saved
        const string outputDir = @"C:\OutputFiles";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the source directory exists; if not, create it and inform the user
        if (!Directory.Exists(sourceDir))
        {
            Console.WriteLine($"Source directory '{sourceDir}' does not exist. Creating it now.");
            Directory.CreateDirectory(sourceDir);
            Console.WriteLine("Please place PDF and matching XML files in the source directory and re‑run the program.");
            return; // Exit – nothing to process yet
        }

        // Get all PDF files in the source directory
        string[] pdfFiles = Directory.GetFiles(sourceDir, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{sourceDir}'. Nothing to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Determine the corresponding XML file (same file name, .xml extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(sourceDir, baseName + ".xml");

            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"Skipping '{pdfPath}': matching XML file not found.");
                continue;
            }

            // Define the output PDF path
            string outputPdfPath = Path.Combine(outputDir, baseName + "_filled.pdf");

            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Bind the XML data to the PDF (fills form fields, XFA, etc.)
                    pdfDoc.BindXml(xmlPath);

                    // Save the updated PDF
                    pdfDoc.Save(outputPdfPath);
                }

                Console.WriteLine($"Processed '{baseName}': XML applied and saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch XML import completed.");
    }
}
