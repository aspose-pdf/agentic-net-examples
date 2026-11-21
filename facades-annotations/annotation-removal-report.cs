using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Suppress known NuGet vulnerability warnings (NU1902, NU1903)
#pragma warning disable NU1902, NU1903

class AnnotationRemovalReport
{
    static void Main()
    {
        // Resolve base directory to make the sample portable
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "CleanedPdfs");
        string reportPath = Path.Combine(baseDir, "AnnotationRemovalReport.txt");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("FileName,AnnotationsRemoved");

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string cleanedPath = Path.Combine(outputFolder, fileName);

            int annotationCount = 0;

            // Count existing annotations (deterministic disposal via using)
            using (Document doc = new Document(pdfPath))
            {
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    annotationCount += page.Annotations.Count;
                }
            }

            // Delete all annotations using PdfAnnotationEditor (Facades API)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);
                editor.DeleteAnnotations();
                editor.Save(cleanedPath);
            }

            // Append result to the report
            reportBuilder.AppendLine($"{fileName},{annotationCount}");
        }

        // Write the report to disk
        File.WriteAllText(reportPath, reportBuilder.ToString());
        Console.WriteLine($"Annotation removal report written to: {reportPath}");
    }
}
#pragma warning restore NU1902, NU1903