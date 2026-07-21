using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfAnnotationEditor resides here

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where cleaned PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        int totalFiles = pdfFiles.Length;

        if (totalFiles == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF and display progress
        for (int i = 0; i < totalFiles; i++)
        {
            string inputPath = pdfFiles[i];
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_clean.pdf");

            // Delete all annotations using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);          // Load the PDF
                editor.DeleteAnnotations();         // Remove all annotations
                editor.Save(outputPath);            // Save the cleaned PDF
            }

            // Calculate and display progress percentage
            int percent = (i + 1) * 100 / totalFiles;
            Console.WriteLine($"Processed {i + 1}/{totalFiles} ({percent}%) - {Path.GetFileName(inputPath)}");
        }

        Console.WriteLine("Batch annotation deletion completed.");
    }
}