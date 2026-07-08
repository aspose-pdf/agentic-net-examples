using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfAsyncDemo
{
    public static class AsyncAnnotationHelper
    {
        // Asynchronously flattens all annotations in a PDF document.
        public static Task FlattenAnnotationsAsync(string inputPdfPath, string outputPdfPath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Load the source PDF (load rule).
                using (Document doc = new Document(inputPdfPath))
                {
                    // Initialize the annotation editor facade.
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(doc);                     // Bind the document.
                        editor.FlatteningAnnotations();          // Flatten all annotations.
                        editor.Save(outputPdfPath);               // Save the modified PDF (save rule).
                    }
                }
            }, cancellationToken);
        }

        // Asynchronously deletes all annotations from a PDF document.
        public static Task DeleteAllAnnotationsAsync(string inputPdfPath, string outputPdfPath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (Document doc = new Document(inputPdfPath))
                {
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(doc);
                        editor.DeleteAnnotations();               // Remove every annotation.
                        editor.Save(outputPdfPath);
                    }
                }
            }, cancellationToken);
        }

        // Asynchronously imports annotations from an FDF file into a PDF document.
        public static Task ImportAnnotationsFromFdfAsync(string inputPdfPath, string fdfFilePath, string outputPdfPath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (Document doc = new Document(inputPdfPath))
                {
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(doc);
                        editor.ImportAnnotationsFromFdf(fdfFilePath); // Import FDF annotations.
                        editor.Save(outputPdfPath);
                    }
                }
            }, cancellationToken);
        }

        // Asynchronously exports all annotations of a PDF document to an XFDF file.
        public static Task ExportAnnotationsToXfdfAsync(string inputPdfPath, string xfdfOutputPath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (Document doc = new Document(inputPdfPath))
                {
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(doc);
                        // Export annotations to the specified XFDF stream (file stream used here).
                        using (FileStream xfdfStream = new FileStream(xfdfOutputPath, FileMode.Create, FileAccess.Write))
                        {
                            editor.ExportAnnotationsToXfdf(xfdfStream);
                        }
                    }
                }
            }, cancellationToken);
        }
    }

    // Minimal entry point to satisfy the compiler. The program does not perform any work by default.
    internal class Program
    {
        // Async Main is allowed and returns a Task.
        private static async Task Main(string[] args)
        {
            // Example placeholder – can be removed or expanded.
            Console.WriteLine("AsyncAnnotationHelper library loaded. No operation executed.");
            await Task.CompletedTask;
        }
    }
}
