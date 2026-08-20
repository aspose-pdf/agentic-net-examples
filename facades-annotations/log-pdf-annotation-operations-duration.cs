using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AnnotationPerformanceMonitoring
{
    // Simple logger that records operation name and duration.
    public class OperationLogger
    {
        private readonly List<string> _entries = new List<string>();

        public void Log(string operationName, TimeSpan duration)
        {
            string entry = $"{DateTime.UtcNow:u} | {operationName} | {duration.TotalMilliseconds} ms";
            _entries.Add(entry);
            Console.WriteLine(entry);
        }

        public IEnumerable<string> GetLog() => _entries;
    }

    // Wrapper around PdfAnnotationEditor that measures execution time of each annotation operation.
    public class PdfAnnotationEditorWithTiming : IDisposable
    {
        private readonly PdfAnnotationEditor _editor;
        private readonly OperationLogger _logger;

        public PdfAnnotationEditorWithTiming(OperationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _editor = new PdfAnnotationEditor();
        }

        // Binds a PDF file to the editor using a stream overload (required by newer Aspose.Pdf versions).
        public void Bind(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided", nameof(pdfPath));

            var sw = Stopwatch.StartNew();
            using (FileStream fs = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
            {
                _editor.BindPdf(fs);
            }
            sw.Stop();
            _logger.Log(nameof(Bind), sw.Elapsed);
        }

        // Deletes a specific annotation by its name.
        public void DeleteAnnotation(string annotationName)
        {
            var sw = Stopwatch.StartNew();
            _editor.DeleteAnnotation(annotationName);
            sw.Stop();
            _logger.Log(nameof(DeleteAnnotation), sw.Elapsed);
        }

        // Deletes all annotations in the document.
        public void DeleteAllAnnotations()
        {
            var sw = Stopwatch.StartNew();
            _editor.DeleteAnnotations();
            sw.Stop();
            _logger.Log(nameof(DeleteAllAnnotations), sw.Elapsed);
        }

        // Exports all annotations to an XFDF file using the stream overload.
        public void ExportAnnotations(string xfdfPath)
        {
            if (string.IsNullOrEmpty(xfdfPath))
                throw new ArgumentException("XFDF path must be provided", nameof(xfdfPath));

            var sw = Stopwatch.StartNew();
            using (FileStream fs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                _editor.ExportAnnotationsToXfdf(fs);
            }
            sw.Stop();
            _logger.Log(nameof(ExportAnnotations), sw.Elapsed);
        }

        // Flattens all annotations.
        public void FlattenAllAnnotations()
        {
            var sw = Stopwatch.StartNew();
            _editor.FlatteningAnnotations();
            sw.Stop();
            _logger.Log(nameof(FlattenAllAnnotations), sw.Elapsed);
        }

        // Saves the modified PDF document using a stream overload (required by newer Aspose.Pdf versions).
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path must be provided", nameof(outputPath));

            var sw = Stopwatch.StartNew();
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                _editor.Save(fs);
            }
            sw.Stop();
            _logger.Log(nameof(Save), sw.Elapsed);
        }

        public void Dispose()
        {
            _editor?.Close();
            _editor?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";
            const string outputPdf = "output.pdf";
            const string exportXfdf = "annotations.xfdf";

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                return;
            }

            OperationLogger logger = new OperationLogger();

            // Load the PDF document using the recommended using pattern.
            using (Document doc = new Document(inputPdf))
            {
                // Perform annotation operations via the timed wrapper.
                using (PdfAnnotationEditorWithTiming editor = new PdfAnnotationEditorWithTiming(logger))
                {
                    editor.Bind(inputPdf);

                    // Example operations.
                    editor.DeleteAllAnnotations();
                    editor.ExportAnnotations(exportXfdf);
                    editor.FlattenAllAnnotations();

                    // Save the modified document.
                    editor.Save(outputPdf);
                }

                // If additional non‑annotation changes are needed, they can be done here.
                // Ensure the document is saved using the standard save rule.
                doc.Save(outputPdf);
            }

            Console.WriteLine("Annotation operations completed. Log entries:");
            foreach (var entry in logger.GetLog())
            {
                // Already printed during logging; this is just for completeness.
            }
        }
    }
}
