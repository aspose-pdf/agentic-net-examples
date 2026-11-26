using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        string inputFolder = @"inputFolderD";

        // Folder where PDF/E‑1 compliant PDFs will be written
        string outputFolder = Path.Combine(inputFolder, "PDFE_Output");
        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + "_PDFE.pdf");
            string logPath = Path.Combine(outputFolder, fileNameWithoutExt + "_conversion_log.xml");

            try
            {
                // Load the source PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Convert the document to PDF/E‑1 compliance.
                    // The conversion log is optional but useful for troubleshooting.
                    doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                    // Save the converted document as a regular PDF (PDF/E‑1 is a PDF variant)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Converted '{pdfPath}' → '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}