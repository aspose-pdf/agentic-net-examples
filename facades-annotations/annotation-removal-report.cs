using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AnnotationRemovalReport
{
    static void Main()
    {
        // Input PDF files (could be read from a directory)
        string[] pdfFiles = new string[]
        {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Directory to store PDFs after annotation removal
        string cleanedDir = "CleanedPdfs";
        Directory.CreateDirectory(cleanedDir);

        // Path for the report file
        string reportPath = "AnnotationRemovalReport.txt";

        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("FileName,AnnotationsRemoved");

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Count annotations before deletion
            int totalAnnotations = 0;
            using (Document doc = new Document(inputPath))
            {
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    totalAnnotations += page.Annotations.Count;
                }
            }

            // Delete all annotations using PdfAnnotationEditor
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string cleanedPath = Path.Combine(cleanedDir, $"{fileName}_cleaned.pdf");

            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPath);
            editor.DeleteAnnotations(); // removes all annotations
            editor.Save(cleanedPath);
            editor.Close(); // optional, releases resources

            // Record the number of removed annotations
            reportBuilder.AppendLine($"{Path.GetFileName(inputPath)},{totalAnnotations}");
            Console.WriteLine($"Processed '{inputPath}': removed {totalAnnotations} annotations.");
        }

        // Write the report to a text file
        File.WriteAllText(reportPath, reportBuilder.ToString());
        Console.WriteLine($"Report saved to '{reportPath}'.");
    }
}