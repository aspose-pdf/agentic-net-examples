using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsyncPdfAnnotations
{
    public static class AsyncAnnotationHelper
    {
        /// <summary>
        /// Asynchronously flattens all annotations in a PDF document.
        /// </summary>
        public static Task FlattenAnnotationsAsync(string inputPdfPath, string outputPdfPath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var editor = new PdfAnnotationEditor();
                editor.BindPdf(inputPdfPath);
                editor.FlatteningAnnotations();
                editor.Save(outputPdfPath);
            }, cancellationToken);
        }

        /// <summary>
        /// Asynchronously deletes all annotations from a PDF document.
        /// </summary>
        public static Task DeleteAllAnnotationsAsync(string inputPdfPath, string outputPdfPath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var editor = new PdfAnnotationEditor();
                editor.BindPdf(inputPdfPath);
                editor.DeleteAnnotations();
                editor.Save(outputPdfPath);
            }, cancellationToken);
        }

        /// <summary>
        /// Asynchronously imports annotations from an FDF file into a PDF document.
        /// </summary>
        public static Task ImportAnnotationsFromFdfAsync(string inputPdfPath, string fdfFilePath, string outputPdfPath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var editor = new PdfAnnotationEditor();
                editor.BindPdf(inputPdfPath);
                editor.ImportAnnotationsFromFdf(fdfFilePath);
                editor.Save(outputPdfPath);
            }, cancellationToken);
        }

        /// <summary>
        /// Asynchronously exports all annotations from a PDF document to an XFDF stream.
        /// </summary>
        public static Task ExportAnnotationsToXfdfAsync(string inputPdfPath, Stream xfdfOutputStream, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var editor = new PdfAnnotationEditor();
                editor.BindPdf(inputPdfPath);
                editor.ExportAnnotationsToXfdf(xfdfOutputStream);
            }, cancellationToken);
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // No operation – the class library functionality is accessed via AsyncAnnotationHelper.
            await Task.CompletedTask;
        }
    }
}