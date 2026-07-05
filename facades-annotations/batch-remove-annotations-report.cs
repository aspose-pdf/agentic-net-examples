using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where the cleaned PDFs will be saved
        const string outputFolder = "OutputPdfs";

        // Verify that the input directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Collect all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // List to hold report lines
        List<string> reportLines = new List<string>();

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            int annotationCount = 0;

            // Count existing annotations (1‑based page indexing)
            using (Document doc = new Document(inputPath))
            {
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    annotationCount += doc.Pages[pageIndex].Annotations.Count;
                }
            }

            // Delete all annotations using PdfAnnotationEditor
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPath);
            editor.DeleteAnnotations();

            // Save the cleaned PDF to the output folder
            string outputPath = Path.Combine(outputFolder, fileName);
            editor.Save(outputPath);
            editor.Close(); // Release resources held by the facade

            // Record the result for this file
            reportLines.Add($"{fileName}: {annotationCount} annotations removed.");
        }

        // Write the summary report to a text file (if any files were processed)
        const string reportPath = "annotation_removal_report.txt";
        if (reportLines.Count > 0)
        {
            File.WriteAllLines(reportPath, reportLines);
        }
        else
        {
            // Create an empty report file to indicate that the run completed without processing files.
            File.WriteAllText(reportPath, "No PDF files were found in the input folder.");
        }

        // Also output the report to the console
        foreach (string line in reportLines)
        {
            Console.WriteLine(line);
        }
    }
}
