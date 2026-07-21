using System;
using System.IO;
using Aspose.Pdf.Facades;

class DiagnosticExporter
{
    /// <summary>
    /// Exports all annotations of a PDF to an XFDF file and writes the same XFDF content to a log file.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF document.</param>
    /// <param name="xfdfPath">Path where the XFDF file will be saved.</param>
    /// <param name="logPath">Path to the diagnostic log file that will contain the XFDF content.</param>
    public static void ExportAnnotationsWithLog(string pdfPath, string xfdfPath, string logPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Use PdfAnnotationEditor from Aspose.Pdf.Facades to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor.
            editor.BindPdf(pdfPath);

            // Export annotations to a memory stream so we can write the same data to two files.
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations to the memory stream in XFDF format.
                editor.ExportAnnotationsToXfdf(xfdfStream);

                // Ensure the stream position is at the beginning before reading.
                xfdfStream.Position = 0;

                // Write the XFDF content to the designated XFDF file.
                using (FileStream xfdfFile = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(xfdfFile);
                }

                // Reset position again to copy to the log file.
                xfdfStream.Position = 0;

                // Write the same XFDF content to the diagnostic log file.
                using (FileStream logFile = new FileStream(logPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(logFile);
                }
            }

            // Close the editor (Dispose is called automatically by the using statement).
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and logged to '{logPath}'.");
    }

    // Example usage
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputXfdf = "sample_annotations.xfdf";
        const string logFile = "xfdf_diagnostic.log";

        ExportAnnotationsWithLog(inputPdf, outputXfdf, logFile);
    }
}