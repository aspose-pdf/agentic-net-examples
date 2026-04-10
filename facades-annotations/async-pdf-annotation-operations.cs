using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class AsyncAnnotationHelper
{
    // Flattens all annotations in a PDF document asynchronously.
    public static Task FlattenAnnotationsAsync(string inputPdfPath, string outputPdfPath, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            // Ensure the operation can be cancelled.
            cancellationToken.ThrowIfCancellationRequested();

            // Load the PDF document.
            using (Document doc = new Document(inputPdfPath))
            {
                // Initialize the annotation editor with the loaded document.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
                {
                    // Flatten all annotations.
                    editor.FlatteningAnnotations();

                    // Save the modified document.
                    editor.Save(outputPdfPath);
                }
            }
        }, cancellationToken);
    }

    // Deletes all annotations in a PDF document asynchronously.
    public static Task DeleteAllAnnotationsAsync(string inputPdfPath, string outputPdfPath, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (Document doc = new Document(inputPdfPath))
            {
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
                {
                    // Delete every annotation.
                    editor.DeleteAnnotations();

                    editor.Save(outputPdfPath);
                }
            }
        }, cancellationToken);
    }

    // Imports annotations from an FDF file into a PDF document asynchronously.
    public static Task ImportAnnotationsFromFdfAsync(string inputPdfPath, string fdfPath, string outputPdfPath, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (Document doc = new Document(inputPdfPath))
            {
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
                {
                    // Import annotations from the specified FDF file.
                    editor.ImportAnnotationsFromFdf(fdfPath);

                    editor.Save(outputPdfPath);
                }
            }
        }, cancellationToken);
    }

    // Exports all annotations from a PDF document to an XFDF file asynchronously.
    public static Task ExportAnnotationsToXfdfAsync(string inputPdfPath, string xfdfOutputPath, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (Document doc = new Document(inputPdfPath))
            {
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
                {
                    // Export annotations to the XFDF file using a stream overload.
                    using (FileStream fs = new FileStream(xfdfOutputPath, FileMode.Create, FileAccess.Write))
                    {
                        editor.ExportAnnotationsToXfdf(fs);
                    }
                }
            }
        }, cancellationToken);
    }

    // Example usage of the asynchronous methods.
    public static async Task ExampleUsageAsync()
    {
        string sourcePdf = "sample.pdf";
        string flattenedPdf = "flattened.pdf";
        string cleanedPdf = "cleaned.pdf";
        string fdfFile = "annotations.fdf";
        string importedPdf = "imported.pdf";
        string xfdfFile = "exported.xfdf";

        // Flatten annotations.
        await FlattenAnnotationsAsync(sourcePdf, flattenedPdf);

        // Delete all annotations.
        await DeleteAllAnnotationsAsync(sourcePdf, cleanedPdf);

        // Import annotations from FDF.
        await ImportAnnotationsFromFdfAsync(sourcePdf, fdfFile, importedPdf);

        // Export annotations to XFDF.
        await ExportAnnotationsToXfdfAsync(sourcePdf, xfdfFile);
    }

    // Entry point required for a console application.
    public static async Task Main(string[] args)
    {
        try
        {
            await ExampleUsageAsync();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
