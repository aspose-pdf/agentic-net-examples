using System;
using System.IO;
using System.Drawing; // System.Drawing.Rectangle and System.Drawing.Color are required by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class SimplePdfEditor : IDisposable
{
    private readonly PdfContentEditor _editor;
    private readonly Document _document;
    private bool _disposed;

    public SimplePdfEditor(string inputPath)
    {
        // Load the PDF document
        _document = new Document(inputPath);
        // Initialise the content editor with the loaded document
        _editor = new PdfContentEditor(_document);
    }

    /// <summary>
    /// Adds a simple text annotation on the first page.
    /// </summary>
    /// <param name="rect">Rectangle that defines the annotation bounds (System.Drawing.Rectangle).</param>
    /// <param name="text">Text to display.</param>
    public void AddTextAnnotation(System.Drawing.Rectangle rect, string text)
    {
        // PdfContentEditor.CreateText expects a System.Drawing.Rectangle and a colour string.
        // "Black" is a recognised colour name; opacity 0 means fully opaque (int expected).
        _editor.CreateText(rect, text, "Helvetica", false, "Black", 0);
    }

    /// <summary>
    /// Replaces all occurrences of <paramref name="oldText"/> with <paramref name="newText"/> in the whole document.
    /// </summary>
    public void ReplaceText(string oldText, string newText)
    {
        _editor.ReplaceText(oldText, newText);
    }

    /// <summary>
    /// Saves the edited PDF to <paramref name="outputPath"/>.
    /// </summary>
    public void Save(string outputPath)
    {
        _editor.Save(outputPath);
    }

    #region IDisposable Support
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            // Dispose managed resources
            _editor?.Close();
            _editor?.Dispose();
            _document?.Dispose();
        }
        _disposed = true;
    }
    #endregion
}

public class Program
{
    public static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (var editor = new SimplePdfEditor(inputPath))
        {
            // Use System.Drawing.Rectangle as required by PdfContentEditor
            var rect = new System.Drawing.Rectangle(100, 500, 200, 50);
            editor.AddTextAnnotation(rect, "Hello Aspose");
            editor.ReplaceText("old", "new");
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
