using System;
using System.IO;
using Aspose.Pdf;

class BatchXfdfImporter
{
    static void Main()
    {
        // Folder containing PDF files and their matching XFDF files
        const string inputFolder = @"C:\Docs\Input";
        // Folder where PDFs with imported annotations will be saved
        const string outputFolder = @"C:\Docs\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(inputFolder, fileNameWithoutExt + ".xfdf");

            // If there is no corresponding XFDF file, skip this PDF
            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No XFDF for '{pdfPath}'. Skipping.");
                continue;
            }

            string outputPdfPath = Path.Combine(outputFolder, fileNameWithoutExt + "_annotated.pdf");

            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Import annotations from the XFDF file
                    pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Save the updated PDF (original content is preserved)
                    pdfDoc.Save(outputPdfPath);
                }

                Console.WriteLine($"Annotations imported: '{pdfPath}' -> '{outputPdfPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}