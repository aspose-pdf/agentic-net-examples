using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

/// <summary>
/// Simple wrapper around Aspose.Pdf.Facades.PdfContentEditor to provide a streamlined editing workflow.
/// </summary>
public class SimplePdfEditor : IDisposable
{
    private readonly PdfContentEditor _editor;
    private readonly Document _document;
    private bool _disposed;

    /// <summary>
    /// Loads a PDF file and initializes the underlying PdfContentEditor.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    public SimplePdfEditor(string inputPath)
    {
        // Load PDF using the standard Document constructor (load rule)
        _document = new Document(inputPath);
        // Initialize the editor with the loaded document
        _editor = new PdfContentEditor(_document);
    }

    /// <summary>
    /// Replaces all occurrences of <paramref name="oldText"/> with <paramref name="newText"/> in the entire document.
    /// </summary>
    public void ReplaceText(string oldText, string newText)
    {
        _editor.ReplaceText(oldText, newText);
    }

    /// <summary>
    /// Replaces text on a specific page. Optionally applies a <see cref="TextState"/> for styling.
    /// </summary>
    public void ReplaceText(string oldText, int pageNumber, string newText, TextState textState = null)
    {
        if (textState != null)
        {
            _editor.ReplaceText(oldText, pageNumber, newText, textState);
        }
        else
        {
            _editor.ReplaceText(oldText, pageNumber, newText);
        }
    }

    /// <summary>
    /// Deletes all images from the PDF.
    /// </summary>
    public void DeleteAllImages()
    {
        _editor.DeleteImage();
    }

    /// <summary>
    /// Deletes specific images on a given page.
    /// </summary>
    /// <param name="pageNumber">1‑based page index.</param>
    /// <param name="imageIndexes">Array of image indexes to delete.</param>
    public void DeleteImages(int pageNumber, int[] imageIndexes)
    {
        _editor.DeleteImage(pageNumber, imageIndexes);
    }

    /// <summary>
    /// Adds a file attachment to the PDF without creating an annotation.
    /// </summary>
    /// <param name="filePath">Path to the file to attach.</param>
    /// <param name="description">Description of the attachment.</param>
    public void AddAttachment(string filePath, string description)
    {
        _editor.AddDocumentAttachment(filePath, description);
    }

    /// <summary>
    /// Saves the edited PDF to the specified output path.
    /// </summary>
    /// <param name="outputPath">Destination file path.</param>
    public void Save(string outputPath)
    {
        // Use the Save method provided by SaveableFacade (lifecycle rule)
        _editor.Save(outputPath);
    }

    /// <summary>
    /// Releases all resources held by the editor and the underlying document.
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            // Close the facade first
            _editor?.Close();
            // Dispose the Document (document‑disposal‑with‑using rule)
            _document?.Dispose();
            _disposed = true;
        }
    }
}

// Example usage of the wrapper
class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputPdf = "sample_edited.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            using (SimplePdfEditor editor = new SimplePdfEditor(inputPdf))
            {
                // Replace a phrase throughout the document
                editor.ReplaceText("Hello", "Hi");

                // Delete all images
                editor.DeleteAllImages();

                // Add a text file as an attachment
                editor.AddAttachment("attachment.txt", "Sample attachment");

                // Persist changes
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Edited PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}