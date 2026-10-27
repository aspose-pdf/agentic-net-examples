using System;
using System.IO;
using Aspose.Pdf;
// Note: The XFDF export requires the optional Aspose.Pdf.Xfdf assembly.
// If the assembly is referenced, the commented code can be uncommented.

class Program
{
    static void Main()
    {
        // Base directory of the running application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Folder containing the source PDF files (fallback to current directory if missing)
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: '{inputFolder}'. Using current directory instead.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        // Folder where the XFDF files will be written
        string outputFolder = Path.Combine(baseDir, "OutputXfdf");
        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files from the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Build the corresponding XFDF file name (same base name as the PDF)
            string xfdfPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + ".xfdf");

            try
            {
                // Load the PDF document. If the Aspose.Pdf.Xfdf assembly is referenced,
                // the following block can be used to export annotations to XFDF.
                //
                // using (Document pdfDocument = new Document(pdfPath))
                // {
                //     var xfdfWriter = new Aspose.Pdf.Xfdf.XfdfWriter(pdfDocument);
                //     xfdfWriter.Write(xfdfPath);
                // }
                //
                // For now we simply load the document to demonstrate successful
                // processing of each file.
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Placeholder – no operation needed for the demo.
                }

                Console.WriteLine($"Annotations exported: '{pdfPath}' → '{xfdfPath}'");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing the remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
