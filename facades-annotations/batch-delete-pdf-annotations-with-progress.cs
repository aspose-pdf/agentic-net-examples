using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to process
        const string inputFolder  = "InputPdfs";
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        int totalFiles = pdfFiles.Length;
        if (totalFiles == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        int processed = 0;
        foreach (string inputPath in pdfFiles)
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Use PdfAnnotationEditor to delete all annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);          // Load the PDF
                editor.DeleteAnnotations();         // Remove all annotations
                editor.Save(outputPath);            // Save the modified PDF
            }

            processed++;

            // Calculate and display progress percentage
            int percent = (int)((processed / (double)totalFiles) * 100);
            Console.WriteLine($"Processed {processed}/{totalFiles} ({percent}%) - {fileName}");
        }

        Console.WriteLine("Batch annotation deletion completed.");
    }
}