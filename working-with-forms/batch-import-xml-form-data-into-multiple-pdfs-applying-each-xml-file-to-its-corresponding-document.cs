using System;
using System.IO;
using Aspose.Pdf;

class BatchXmlImport
{
    static void Main()
    {
        // Base directory of the application (works on any OS)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        // Folder containing the source PDFs and matching XML files (relative to the base directory)
        string sourceFolder = Path.Combine(baseDir, "InputFiles");
        // Folder where the PDFs with imported XML data will be saved (relative to the base directory)
        string outputFolder = Path.Combine(baseDir, "OutputFiles");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the source directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder '{sourceFolder}' does not exist. Please create it and add PDF/XML pairs.");
            return;
        }

        // Get all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Determine the corresponding XML file (same file name, .xml extension)
            string xmlPath = Path.ChangeExtension(pdfPath, ".xml");

            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"XML file not found for '{Path.GetFileName(pdfPath)}'. Skipping.");
                continue;
            }

            // Build the output PDF path (same name, placed in output folder)
            string outputPdfPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Bind the XML data to the PDF (imports form data)
                    pdfDoc.BindXml(xmlPath);

                    // Save the updated PDF
                    pdfDoc.Save(outputPdfPath);
                }

                Console.WriteLine($"Processed '{Path.GetFileName(pdfPath)}' -> '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch XML import completed.");
    }
}
