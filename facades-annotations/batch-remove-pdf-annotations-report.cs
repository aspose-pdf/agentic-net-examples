using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AnnotationRemovalReport
{
    // Simple DTO to hold the result for each file
    private class AnnotationResult
    {
        public string FileName { get; set; }
        public int RemovedCount { get; set; }
    }

    static void Main()
    {
        // Input directory containing PDF files to process
        const string inputDirectory = @"C:\InputPdfs";
        // Output directory where cleaned PDFs will be saved
        const string outputDirectory = @"C:\CleanedPdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Collect results: file name -> number of annotations removed
        var removalResults = new List<AnnotationResult>();

        // Process each PDF file in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            int annotationCount = 0;

            // Load the document to count existing annotations
            using (Document doc = new Document(inputPath))
            {
                foreach (Page page in doc.Pages)
                {
                    annotationCount += page.Annotations.Count;
                }
            }

            // Delete all annotations using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                editor.DeleteAnnotations();

                // Save the cleaned PDF
                string outputPath = Path.Combine(
                    outputDirectory,
                    Path.GetFileNameWithoutExtension(inputPath) + "_clean.pdf");

                editor.Save(outputPath);
                // No explicit Close() needed – the using block disposes the editor.
            }

            // Record the result
            removalResults.Add(new AnnotationResult
            {
                FileName = Path.GetFileName(inputPath),
                RemovedCount = annotationCount
            });
        }

        // Generate a simple console report
        Console.WriteLine("Annotation Removal Report");
        Console.WriteLine("--------------------------");
        foreach (var result in removalResults)
        {
            Console.WriteLine($"{result.FileName}: {result.RemovedCount} annotation(s) removed");
        }
    }
}
