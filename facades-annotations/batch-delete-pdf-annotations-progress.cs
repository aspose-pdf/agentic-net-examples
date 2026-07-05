using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputDir = "InputPdfs";
        // Output folder where processed PDFs will be saved
        const string outputDir = "OutputPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        int totalFiles = pdfFiles.Length;

        if (totalFiles == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF file
        for (int i = 0; i < totalFiles; i++)
        {
            string inputPath = pdfFiles[i];
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDir, fileName);

            // Use PdfAnnotationEditor to delete all annotations
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPath);          // Load the PDF
            editor.DeleteAnnotations();         // Remove all annotations
            editor.Save(outputPath);            // Save the cleaned PDF
            editor.Close();                     // Release resources (optional)

            // Update progress bar
            int percent = (i + 1) * 100 / totalFiles;
            Console.Write($"\rProcessed {i + 1}/{totalFiles} files ({percent}%)");
        }

        Console.WriteLine("\nBatch annotation deletion completed.");
    }
}