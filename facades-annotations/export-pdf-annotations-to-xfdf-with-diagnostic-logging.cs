using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfDiagnostic
{
    class DiagnosticExporter
    {
        /// <summary>
        /// Exports all annotations of a PDF document to an XFDF file and writes the same XFDF content
        /// to a separate log file for diagnostic purposes.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <param name="xfdfPath">Path where the XFDF export will be saved.</param>
        /// <param name="logPath">Path to the diagnostic log file that will contain the XFDF content.</param>
        public void ExportAnnotationsAndLog(string pdfPath, string xfdfPath, string logPath)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
                return;
            }

            // Load the PDF document and export annotations using deterministic disposal.
            using (Document doc = new Document(pdfPath))
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations to the memory stream.
                editor.ExportAnnotationsToXfdf(xfdfStream);

                // Reset the stream position before each copy operation.
                xfdfStream.Position = 0;
                using (FileStream xfdfFile = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(xfdfFile);
                }

                xfdfStream.Position = 0;
                using (FileStream logFile = new FileStream(logPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(logFile);
                }
            }

            Console.WriteLine($"Annotations exported to '{xfdfPath}' and logged to '{logPath}'.");
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point required for a console application.
        /// Accepts optional command‑line arguments: <c>pdfPath xfdfPath logPath</c>.
        /// </summary>
        static void Main(string[] args)
        {
            string pdfPath = args.Length > 0 ? args[0] : "input.pdf";
            string xfdfPath = args.Length > 1 ? args[1] : "output.xfdf";
            string logPath = args.Length > 2 ? args[2] : "diagnostic.log";

            var exporter = new DiagnosticExporter();
            exporter.ExportAnnotationsAndLog(pdfPath, xfdfPath, logPath);
        }
    }
}
