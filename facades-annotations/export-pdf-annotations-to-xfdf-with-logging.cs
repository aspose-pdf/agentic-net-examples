using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Paths can be adjusted as needed
    private const string InputPdfPath = "input.pdf";
    private const string ExportXfdfPath = "annotations.xfdf";
    private const string LogFilePath = "xfdf_log.txt";

    static void Main()
    {
        // Verify input PDF exists
        if (!File.Exists(InputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {InputPdfPath}");
            return;
        }

        // Export annotations to XFDF and write the same content to a log file
        ExportAnnotationsWithLogging(InputPdfPath, ExportXfdfPath, LogFilePath);
    }

    private static void ExportAnnotationsWithLogging(string pdfPath, string xfdfPath, string logPath)
    {
        // Use PdfAnnotationEditor from Aspose.Pdf.Facades to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(pdfPath);

            // Export annotations to a memory stream first
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);

                // Ensure the stream is ready for reading
                xfdfStream.Position = 0;

                // Write XFDF content to the designated XFDF file
                using (FileStream fileOut = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(fileOut);
                }

                // Reset stream position again for logging
                xfdfStream.Position = 0;

                // Write the same XFDF content to the log file
                using (StreamWriter logWriter = new StreamWriter(logPath, false))
                {
                    using (StreamReader reader = new StreamReader(xfdfStream))
                    {
                        string xfdfContent = reader.ReadToEnd();
                        logWriter.WriteLine("=== XFDF Export Log ===");
                        logWriter.WriteLine($"Exported from: {pdfPath}");
                        logWriter.WriteLine($"Exported to: {xfdfPath}");
                        logWriter.WriteLine("XFDF Content:");
                        logWriter.WriteLine(xfdfContent);
                    }
                }
            }

            // Close the editor (Dispose is called automatically by using)
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and logged to '{logPath}'.");
    }
}