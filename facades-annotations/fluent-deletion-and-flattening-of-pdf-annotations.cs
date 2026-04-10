using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfProcessor
{
    // Deletes all annotations, flattens any remaining ones, and saves the result.
    // The workflow is expressed as a single fluent‑style chain.
    public static void DeleteAndFlatten(string inputPath, string outputPath)
    {
        // Ensure the source PDF exists – if not, create a minimal placeholder document.
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add(); // add a blank page
                placeholder.Save(inputPath);
            }
        }

        // Use a fluent wrapper around PdfAnnotationEditor so the operations can be chained.
        new FluentAnnotationEditor()
            .Bind(inputPath)
            .DeleteAll()
            .Flatten()
            .Save(outputPath);
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        DeleteAndFlatten(inputPdf, outputPdf);
        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}

/// <summary>
/// Small helper that wraps Aspose.Pdf.Facades.PdfAnnotationEditor with a fluent API.
/// Each method returns the wrapper instance, allowing a single method chain.
/// </summary>
public class FluentAnnotationEditor : IDisposable
{
    private readonly PdfAnnotationEditor _editor = new PdfAnnotationEditor();
    private bool _disposed;

    public FluentAnnotationEditor Bind(string pdfPath)
    {
        _editor.BindPdf(pdfPath);
        return this;
    }

    public FluentAnnotationEditor DeleteAll()
    {
        _editor.DeleteAnnotations();
        return this;
    }

    public FluentAnnotationEditor Flatten()
    {
        _editor.FlatteningAnnotations();
        return this;
    }

    public FluentAnnotationEditor Save(string outputPath)
    {
        _editor.Save(outputPath);
        return this;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _editor.Close();
            _disposed = true;
        }
    }
}