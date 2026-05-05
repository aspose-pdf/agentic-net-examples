using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where PDFs without annotations will be saved
        const string outputFolder = "CleanedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            return;
        }

        // List to hold per‑file removal statistics for the final report
        var report = new List<(string FileName, int RemovedCount)>();

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Count total annotations before deletion
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
                editor.DeleteAnnotations(); // removes all annotations
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));
                editor.Save(outputPath);    // save the cleaned PDF
            }

            Console.WriteLine($"{Path.GetFileName(inputPath)}: removed {annotationCount} annotations.");
            report.Add((Path.GetFileName(inputPath), annotationCount));
        }

        // Output a summary report after batch processing
        Console.WriteLine("\n=== Annotation Removal Summary ===");
        foreach (var entry in report)
        {
            Console.WriteLine($"{entry.FileName}: {entry.RemovedCount} annotations removed");
        }
        Console.WriteLine($"Total files processed: {report.Count}");
    }
}
