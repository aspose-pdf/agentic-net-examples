using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AnnotationRemovalReport
{
    static void Main()
    {
        // Input PDF files (adjust the paths as needed)
        string[] inputFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Directory where cleaned PDFs will be saved
        string outputDirectory = "CleanedPdfs";
        Directory.CreateDirectory(outputDirectory);

        // List to hold report entries
        var report = new List<(string FileName, int RemovedCount)>();

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Bind the PDF to the annotation editor
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPath);

                    // Count total annotations before deletion
                    int totalAnnotations = 0;
                    foreach (Page page in editor.Document.Pages)
                    {
                        totalAnnotations += page.Annotations.Count;
                    }

                    // Delete all annotations
                    editor.DeleteAnnotations();

                    // Save the cleaned PDF
                    string outputPath = Path.Combine(
                        outputDirectory,
                        Path.GetFileNameWithoutExtension(inputPath) + "_clean.pdf");
                    editor.Save(outputPath);

                    // Record the result
                    report.Add((Path.GetFileName(inputPath), totalAnnotations));
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        // Output the summary report
        Console.WriteLine("Annotation Removal Report:");
        foreach (var entry in report)
        {
            Console.WriteLine($"File: {entry.FileName} - Annotations removed: {entry.RemovedCount}");
        }
    }
}