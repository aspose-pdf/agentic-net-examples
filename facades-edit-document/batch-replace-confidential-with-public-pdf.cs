using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "input_pdfs";
        // Folder where the modified PDFs will be saved
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Create a PdfContentEditor facade to edit the document
                    using (PdfContentEditor editor = new PdfContentEditor())
                    {
                        // Bind the loaded document to the editor
                        editor.BindPdf(doc);
                        // Replace every occurrence of "Confidential" with "Public" on all pages
                        editor.ReplaceText("Confidential", "Public");
                    }

                    // Save the modified document back to PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}