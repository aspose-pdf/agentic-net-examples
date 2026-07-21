using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_flattened.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Asynchronously flatten all annotations in the PDF
        await FlattenAnnotationsAsync(inputPdf, outputPdf, CancellationToken.None);

        Console.WriteLine($"Flattened PDF saved to '{outputPdf}'.");
    }

    // Wraps PdfAnnotationEditor operations in Task.Run for non‑blocking execution
    static Task FlattenAnnotationsAsync(string sourcePath, string destPath, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            // Initialize the editor and bind the source PDF
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(sourcePath);

            // Flatten all annotations
            editor.FlatteningAnnotations();

            // Save the modified PDF
            editor.Save(destPath);

            // Release resources
            editor.Close();
        }, cancellationToken);
    }

    // Example: delete all annotations asynchronously
    static Task DeleteAllAnnotationsAsync(string sourcePath, string destPath, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(sourcePath);
            editor.DeleteAnnotations();
            editor.Save(destPath);
            editor.Close();
        }, cancellationToken);
    }

    // Example: import annotations from an FDF file asynchronously
    static Task ImportAnnotationsFromFdfAsync(string sourcePath, string fdfPath, string destPath, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(sourcePath);
            editor.ImportAnnotationsFromFdf(fdfPath);
            editor.Save(destPath);
            editor.Close();
        }, cancellationToken);
    }

    // Example: export annotations to an XFDF file asynchronously
    static Task ExportAnnotationsToXfdfAsync(string sourcePath, string xfdfPath, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(sourcePath);
            using (var stream = File.Create(xfdfPath))
            {
                editor.ExportAnnotationsToXfdf(stream);
            }
            editor.Close();
        }, cancellationToken);
    }
}