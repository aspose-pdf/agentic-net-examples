using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AnnotationRemovalReport
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Resolve folders in a safe, OS‑agnostic way.
        // ---------------------------------------------------------------------
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");
        const string reportFileName = "annotation_report.txt";

        // If the expected InputPdfs folder does not exist, fall back to the current
        // working directory and inform the user. This prevents DirectoryNotFoundException
        // at runtime when the sample is executed out‑of‑the‑box.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' not found. Using current directory as fallback.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        // Ensure the output folder exists – CreateDirectory is a no‑op if it already does.
        Directory.CreateDirectory(outputFolder);

        // ---------------------------------------------------------------------
        // Build the CSV report in memory.
        // ---------------------------------------------------------------------
        using (StringWriter reportWriter = new StringWriter())
        {
            reportWriter.WriteLine("FileName,AnnotationsRemoved");

            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
            if (pdfFiles.Length == 0)
            {
                Console.WriteLine($"No PDF files found in '{inputFolder}'.");
            }

            foreach (string inputFile in pdfFiles)
            {
                try
                {
                    string fileName = Path.GetFileName(inputFile);
                    string outputFile = Path.Combine(outputFolder, fileName);

                    // -----------------------------------------------------------------
                    // 1. Count existing annotations using the high‑level Document API.
                    // -----------------------------------------------------------------
                    int removedCount = 0;
                    using (Document pdfDoc = new Document(inputFile))
                    {
                        foreach (Page page in pdfDoc.Pages)
                        {
                            removedCount += page.Annotations.Count;
                        }
                    }

                    // -----------------------------------------------------------------
                    // 2. Delete all annotations and save the cleaned PDF.
                    // -----------------------------------------------------------------
                    using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
                    {
                        annotEditor.BindPdf(inputFile);
                        annotEditor.DeleteAnnotations();
                        annotEditor.Save(outputFile);
                    }

                    // Record the result in the CSV report.
                    reportWriter.WriteLine($"{fileName},{removedCount}");
                }
                catch (Exception ex)
                {
                    // Log the error but continue processing the remaining files.
                    Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
                }
            }

            // Write the report next to the executable (or wherever the base directory points).
            string reportPath = Path.Combine(baseDir, reportFileName);
            File.WriteAllText(reportPath, reportWriter.ToString());
        }

        Console.WriteLine("Annotation removal completed. Report saved to 'annotation_report.txt'.");
    }
}
