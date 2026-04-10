using System;
using System.Drawing;                     // Required for Rectangle and Color used by PdfContentEditor
using Aspose.Pdf.Facades;                // Facade API for PDF content editing

// Wrapper class that simplifies common PdfContentEditor operations.
// It encapsulates binding, editing, and saving of a PDF document.
public class PdfEditorWrapper : IDisposable
{
    private readonly PdfContentEditor _editor;

    // Constructor binds the specified PDF file for editing.
    public PdfEditorWrapper(string inputPdfPath)
    {
        _editor = new PdfContentEditor();
        _editor.BindPdf(inputPdfPath);    // Load PDF into the facade
    }

    // Adds a markup annotation (highlight, underline, strikeout, squiggly) on a given page.
    // type: 0=Highlight, 1=Underline, 2=StrikeOut, 3=Squiggly
    public void AddMarkup(int pageNumber, Rectangle bounds, int type, Color color, string contents = "")
    {
        // PdfContentEditor expects System.Drawing.Rectangle and System.Drawing.Color.
        _editor.CreateMarkup(bounds, contents, type, pageNumber, color);
    }

    // Convenience method for adding a highlight annotation.
    public void AddHighlight(int pageNumber, Rectangle bounds, Color color)
    {
        AddMarkup(pageNumber, bounds, 0, color, "Highlight");
    }

    // Convenience method for adding an underline annotation.
    public void AddUnderline(int pageNumber, Rectangle bounds, Color color)
    {
        AddMarkup(pageNumber, bounds, 1, color, "Underline");
    }

    // Adds a free‑text annotation on the specified page.
    public void AddFreeText(int pageNumber, Rectangle bounds, string text)
    {
        _editor.CreateFreeText(bounds, text, pageNumber);
    }

    // Adds a text annotation (with title, contents, open flag and icon) on the specified page.
    public void AddTextAnnotation(int pageNumber, Rectangle bounds, string title, string contents, bool open, string icon)
    {
        _editor.CreateText(bounds, title, contents, open, icon, pageNumber);
    }

    // Replaces all occurrences of oldText with newText throughout the document.
    public void ReplaceAllText(string oldText, string newText)
    {
        _editor.ReplaceText(oldText, newText);
    }

    // Replaces text on a specific page.
    public void ReplaceTextOnPage(int pageNumber, string oldText, string newText)
    {
        _editor.ReplaceText(oldText, pageNumber, newText);
    }

    // Deletes all images from the document.
    public void DeleteAllImages()
    {
        _editor.DeleteImage();
    }

    // Deletes specific images on a given page (imageIndexes are zero‑based indexes of images on that page).
    public void DeleteImagesOnPage(int pageNumber, int[] imageIndexes)
    {
        _editor.DeleteImage(pageNumber, imageIndexes);
    }

    // Saves the edited PDF to the specified output path.
    public void Save(string outputPdfPath)
    {
        _editor.Save(outputPdfPath);
    }

    // Dispose the underlying facade.
    public void Dispose()
    {
        _editor?.Dispose();
    }
}

// Example usage of the wrapper.
class Program
{
    static void Main()
    {
        const string inputPath  = "sample.pdf";
        const string outputPath = "sample_edited.pdf";

        // Ensure the input file exists before proceeding.
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use the wrapper inside a using block for deterministic disposal.
        using (var pdfEditor = new PdfEditorWrapper(inputPath))
        {
            // Add a yellow highlight on page 1.
            pdfEditor.AddHighlight(
                pageNumber: 1,
                bounds: new Rectangle(100, 500, 200, 20),
                color: Color.Yellow);

            // Add a text annotation on page 2.
            pdfEditor.AddTextAnnotation(
                pageNumber: 2,
                bounds: new Rectangle(150, 400, 250, 100),
                title: "Note",
                contents: "Review this section.",
                open: true,
                icon: "Comment");

            // Replace a placeholder string throughout the document.
            pdfEditor.ReplaceAllText("{PLACEHOLDER}", "Actual Value");

            // Delete all images on page 3.
            pdfEditor.DeleteImagesOnPage(pageNumber: 3, imageIndexes: new int[] { 0, 1 });

            // Persist changes.
            pdfEditor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}