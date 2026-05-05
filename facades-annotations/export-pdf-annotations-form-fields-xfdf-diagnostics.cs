using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class DiagnosticExporter
{
    // Exports annotations and form fields to XFDF and writes the XFDF content to a log file.
    public static void ExportPdfWithDiagnostics(string pdfPath, string annotationsXfdfPath, string formXfdfPath, string logPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // ---------- Export Annotations ----------
            // Use PdfAnnotationEditor (Facades API) to export all annotations.
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor(doc))
            {
                // Export annotations to a memory stream.
                using (MemoryStream annotStream = new MemoryStream())
                {
                    annotEditor.ExportAnnotationsToXfdf(annotStream);

                    // Reset stream position for reading.
                    annotStream.Position = 0;

                    // Save the XFDF file (optional, can be omitted if only logging is needed).
                    using (FileStream xfdfFile = new FileStream(annotationsXfdfPath, FileMode.Create, FileAccess.Write))
                    {
                        annotStream.CopyTo(xfdfFile);
                    }

                    // Write the same XFDF content to the diagnostic log file.
                    annotStream.Position = 0; // Reset again before copying.
                    using (FileStream logFile = new FileStream(logPath, FileMode.Append, FileAccess.Write))
                    {
                        // Add a header to separate sections.
                        using (StreamWriter writer = new StreamWriter(logFile, System.Text.Encoding.UTF8, 1024, true))
                        {
                            writer.WriteLine("=== Annotations XFDF Export ===");
                            writer.WriteLine($"Source PDF: {pdfPath}");
                            writer.WriteLine($"Exported XFDF File: {annotationsXfdfPath}");
                            writer.WriteLine("XFDF Content:");
                            writer.Flush(); // Ensure header is written before the raw XFDF.
                        }

                        // Append raw XFDF bytes.
                        annotStream.CopyTo(logFile);
                        // Add a newline for readability.
                        using (StreamWriter writer = new StreamWriter(logFile, System.Text.Encoding.UTF8, 1024, true))
                        {
                            writer.WriteLine();
                            writer.WriteLine();
                        }
                    }
                }
            }

            // ---------- Export Form Fields ----------
            // Use Form (Facades API) to export form field data to XFDF.
            using (Form form = new Form(doc))
            {
                using (MemoryStream formStream = new MemoryStream())
                {
                    form.ExportXfdf(formStream);
                    formStream.Position = 0;

                    // Save the XFDF file for form fields (optional).
                    using (FileStream xfdfFile = new FileStream(formXfdfPath, FileMode.Create, FileAccess.Write))
                    {
                        formStream.CopyTo(xfdfFile);
                    }

                    // Append form XFDF content to the same diagnostic log.
                    formStream.Position = 0;
                    using (FileStream logFile = new FileStream(logPath, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(logFile, System.Text.Encoding.UTF8, 1024, true))
                        {
                            writer.WriteLine("=== Form Fields XFDF Export ===");
                            writer.WriteLine($"Source PDF: {pdfPath}");
                            writer.WriteLine($"Exported XFDF File: {formXfdfPath}");
                            writer.WriteLine("XFDF Content:");
                            writer.Flush();
                        }

                        formStream.CopyTo(logFile);
                        using (StreamWriter writer = new StreamWriter(logFile, System.Text.Encoding.UTF8, 1024, true))
                        {
                            writer.WriteLine();
                            writer.WriteLine();
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Diagnostic export completed. Log written to '{logPath}'.");
    }

    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string annotationsXfdf = "annotations.xfdf";
        const string formXfdf = "formfields.xfdf";
        const string diagnosticLog = "xfdf_diagnostic.log";

        ExportPdfWithDiagnostics(inputPdf, annotationsXfdf, formXfdf, diagnosticLog);
    }
}