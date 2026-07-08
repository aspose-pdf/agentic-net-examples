using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfEditingWrapper
{
    /// <summary>
    /// Simplified wrapper around Aspose.Pdf.Facades.PdfContentEditor.
    /// Provides common editing operations with a clean API.
    /// </summary>
    public class PdfEditorWrapper : IDisposable
    {
        private readonly PdfContentEditor _editor;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the wrapper.
        /// </summary>
        public PdfEditorWrapper()
        {
            // Create the underlying PdfContentEditor instance.
            _editor = new PdfContentEditor();
        }

        /// <summary>
        /// Binds a PDF file for editing.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF.</param>
        public void Load(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // Bind the PDF file; this prepares the editor for subsequent operations.
            _editor.BindPdf(pdfPath);
        }

        /// <summary>
        /// Replaces all occurrences of <paramref name="oldText"/> with <paramref name="newText"/> in the entire document.
        /// </summary>
        public void ReplaceText(string oldText, string newText)
        {
            if (oldText == null) throw new ArgumentNullException(nameof(oldText));
            if (newText == null) throw new ArgumentNullException(nameof(newText));

            // Simple replace across all pages.
            _editor.ReplaceText(oldText, newText);
        }

        /// <summary>
        /// Replaces text on a specific page.
        /// </summary>
        /// <param name="oldText">Text to be replaced.</param>
        /// <param name="newText">Replacement text.</param>
        /// <param name="pageNumber">1‑based page index.</param>
        public void ReplaceTextOnPage(string oldText, string newText, int pageNumber)
        {
            if (oldText == null) throw new ArgumentNullException(nameof(oldText));
            if (newText == null) throw new ArgumentNullException(nameof(newText));
            if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page numbers are 1‑based.");

            // Replace text on the specified page.
            _editor.ReplaceText(oldText, pageNumber, newText);
        }

        /// <summary>
        /// Adds a web link annotation to the specified rectangle.
        /// </summary>
        /// <param name="rect">Location and size of the link (Aspose.Pdf.Rectangle).</param>
        /// <param name="url">Target URL.</param>
        /// <param name="color">Optional link border color; defaults to black if null.</param>
        public void AddWebLink(Aspose.Pdf.Rectangle rect, string url, System.Drawing.Color? color = null)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL must be provided.", nameof(url));

            // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle because PdfContentEditor expects the latter.
            var sysRect = new System.Drawing.Rectangle(
                (int)rect.LLX,
                (int)rect.LLY,
                (int)(rect.URX - rect.LLX),
                (int)(rect.URY - rect.LLY));

            // The third parameter is the page number where the link is placed.
            // Use 0 to indicate the current page context (the editor determines it internally).
            if (color.HasValue)
                _editor.CreateWebLink(sysRect, url, 0, color.Value);
            else
                _editor.CreateWebLink(sysRect, url, 0);
        }

        /// <summary>
        /// Saves the edited PDF to the specified path.
        /// </summary>
        /// <param name="outputPath">Destination file path.</param>
        public void Save(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Save the modified document.
            _editor.Save(outputPath);
        }

        /// <summary>
        /// Releases all resources used by the wrapper.
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;

            // Close and dispose the underlying facade.
            _editor?.Close();
            _editor?.Dispose();

            _isDisposed = true;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a real library you would change the project output type to "Class Library".
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – the wrapper is intended to be used by client code.
        }
    }

    // Example usage (for illustration; remove or comment out in production code):
    // class Demo
    // {
    //     static void Main()
    //     {
    //         const string inputPdf = "input.pdf";
    //         const string outputPdf = "output.pdf";
    //
    //         using (PdfEditorWrapper wrapper = new PdfEditorWrapper())
    //         {
    //             wrapper.Load(inputPdf);
    //             wrapper.ReplaceText("Hello", "Hi");
    //             Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
    //             wrapper.AddWebLink(linkRect, "https://example.com", System.Drawing.Color.Blue);
    //             wrapper.Save(outputPdf);
    //         }
    //     }
    // }
}
