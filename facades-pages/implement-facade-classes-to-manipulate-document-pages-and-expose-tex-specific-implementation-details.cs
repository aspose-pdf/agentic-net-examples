using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for TeXSaveOptions resides in Aspose.Pdf namespace, but keep for completeness

// Facade class that provides page manipulation via PdfPageEditor
// and allows exporting the result to TeX format using TeXSaveOptions.
public class PdfTeXProcessor : IDisposable
{
    private readonly Document _document;
    private readonly PdfPageEditor _pageEditor;
    private bool _disposed;

    // Load a PDF document from a file path.
    // Uses the standard Document constructor (load rule).
    public PdfTeXProcessor(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        // Load the PDF document.
        _document = new Document(pdfPath);

        // Initialize the page editor facade with the loaded document.
        _pageEditor = new PdfPageEditor(_document);
    }

    // Rotate selected pages. Rotation must be 0, 90, 180 or 270.
    public void RotatePages(int[] pageNumbers, int rotationDegree)
    {
        if (pageNumbers == null || pageNumbers.Length == 0)
            throw new ArgumentException("At least one page number must be specified.", nameof(pageNumbers));

        // Set the pages to be processed.
        _pageEditor.ProcessPages = pageNumbers;

        // Set the rotation value.
        _pageEditor.Rotation = rotationDegree;

        // Apply the changes to the document.
        _pageEditor.ApplyChanges();
    }

    // Set a uniform zoom factor for the whole document.
    public void SetZoom(double zoomFactor)
    {
        // Zoom must be greater than 0.
        if (zoomFactor <= 0)
            throw new ArgumentOutOfRangeException(nameof(zoomFactor), "Zoom factor must be positive.");

        // PdfPageEditor.Zoom expects a float, so cast explicitly.
        _pageEditor.Zoom = (float)zoomFactor;

        // Apply the changes (no specific page list needed – applies to all pages).
        _pageEditor.ApplyChanges();
    }

    // Export the current document state to TeX format.
    // Returns the number of pages written (available via TeXSaveOptions.PagesCount).
    public int SaveAsTeX(string outputPath)
    {
        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Output path must be provided.", nameof(outputPath));

        // Initialize TeX save options.
        var texOptions = new TeXSaveOptions();

        // Save the document using the TeX options (save rule).
        _document.Save(outputPath, texOptions);

        // Return the page count after conversion.
        return texOptions.PagesCount;
    }

    // Expose some TeX‑specific information without performing a save.
    // For example, you can query the default cache‑glyphs setting.
    public bool GetTeXCacheGlyphsSetting()
    {
        var texOptions = new TeXSaveOptions();
        return texOptions.CacheGlyphs;
    }

    // Dispose pattern – closes the facade and releases the document.
    public void Dispose()
    {
        if (_disposed) return;

        // Close the page editor (calls Close on the underlying document).
        _pageEditor?.Close();

        // Dispose the document (ensures file handles are released).
        _document?.Dispose();

        _disposed = true;
    }
}

// Example usage
class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputTex = "sample.tex";

        // Ensure the input file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use the processor inside a using block to guarantee disposal.
        using (var processor = new PdfTeXProcessor(inputPdf))
        {
            // Rotate pages 2 and 3 by 90 degrees.
            processor.RotatePages(new[] { 2, 3 }, 90);

            // Set zoom to 150% for the whole document.
            processor.SetZoom(1.5);

            // Export to TeX and obtain the number of pages written.
            int texPageCount = processor.SaveAsTeX(outputTex);
            Console.WriteLine($"Saved TeX file '{outputTex}' with {texPageCount} pages.");

            // Optional: read a TeX‑specific option.
            bool cacheGlyphs = processor.GetTeXCacheGlyphsSetting();
            Console.WriteLine($"TeX CacheGlyphs option default: {cacheGlyphs}");
        }
    }
}