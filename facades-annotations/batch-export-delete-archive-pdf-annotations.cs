using System;
using System.IO;
using Aspose.Pdf.Facades;

class AnnotationBatchProcessor
{
    static void Main()
    {
        // Define directories
        string inputDir   = @"C:\PdfBatch\Input";
        string outputDir  = @"C:\PdfBatch\Processed";
        string archiveDir = @"C:\PdfBatch\Archive";

        // Ensure all required directories exist (including the input folder)
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);
        Directory.CreateDirectory(archiveDir);

        // Get all PDF files in the input directory (empty array if none)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDir}'. Nothing to process.");
            return;
        }

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                string fileName   = Path.GetFileNameWithoutExtension(pdfPath);
                string xfdfPath   = Path.Combine(archiveDir, fileName + ".xfdf");
                string outPdfPath = Path.Combine(outputDir, fileName + ".pdf");

                // Use PdfAnnotationEditor to work with annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(pdfPath);

                    // Export all annotations to XFDF file
                    using (FileStream xfdfStream = File.Create(xfdfPath))
                    {
                        editor.ExportAnnotationsToXfdf(xfdfStream);
                    }

                    // Delete all annotations from the document
                    editor.DeleteAnnotations();

                    // Save the PDF without annotations to the processed folder
                    editor.Save(outPdfPath);
                }

                Console.WriteLine($"Processed '{pdfPath}'. XFDF archived to '{xfdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation export, deletion, and archiving completed.");
    }
}
