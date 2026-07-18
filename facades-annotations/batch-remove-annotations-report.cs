using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to process
        const string inputDir = "InputPdfs";
        // Output directory for cleaned PDFs
        const string outputDir = "CleanedPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        Console.WriteLine("Annotation removal report:");
        Console.WriteLine("---------------------------");

        foreach (string inputPath in pdfFiles)
        {
            // Count annotations before deletion
            int annotationCount = 0;
            using (Document doc = new Document(inputPath))
            {
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    annotationCount += doc.Pages[pageIndex].Annotations.Count;
                }
            }

            // Delete all annotations using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                editor.DeleteAnnotations();

                string outputPath = Path.Combine(
                    outputDir,
                    Path.GetFileNameWithoutExtension(inputPath) + "_clean.pdf");

                editor.Save(outputPath);
            }

            // Report the number of annotations removed for this file
            Console.WriteLine($"{Path.GetFileName(inputPath)}: {annotationCount} annotation(s) removed.");
        }

        Console.WriteLine("Processing completed.");
    }
}