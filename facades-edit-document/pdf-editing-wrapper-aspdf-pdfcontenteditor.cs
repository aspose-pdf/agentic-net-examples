using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfEditingWrapper
{
    /// <summary>
    /// Simplifies common PDF editing tasks by encapsulating Aspose.Pdf.Facades.PdfContentEditor.
    /// </summary>
    public class PdfEditorWrapper : IDisposable
    {
        private readonly PdfContentEditor _editor;
        private bool _disposed;

        /// <summary>
        /// Binds the specified PDF file for editing.
        /// </summary>
        /// <param name="inputPath">Path to the source PDF.</param>
        public PdfEditorWrapper(string inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input path must be provided.", nameof(inputPath));

            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPath}");

            // Create the facade and bind the PDF file.
            _editor = new PdfContentEditor();
            _editor.BindPdf(inputPath);
        }

        /// <summary>
        /// Replaces all occurrences of <paramref name="oldText"/> with <paramref name="newText"/> in the whole document.
        /// </summary>
        public void ReplaceText(string oldText, string newText)
        {
            if (oldText == null) throw new ArgumentNullException(nameof(oldText));
            if (newText == null) throw new ArgumentNullException(nameof(newText));

            _editor.ReplaceText(oldText, newText);
        }

        /// <summary>
        /// Replaces <paramref name="oldText"/> with <paramref name="newText"/> on a specific page.
        /// </summary>
        /// <param name="oldText">Text to be replaced.</param>
        /// <param name="pageNumber">1‑based page index.</param>
        /// <param name="newText">Replacement text.</param>
        public void ReplaceText(string oldText, int pageNumber, string newText)
        {
            if (oldText == null) throw new ArgumentNullException(nameof(oldText));
            if (newText == null) throw new ArgumentNullException(nameof(newText));
            if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page numbers are 1‑based.");

            _editor.ReplaceText(oldText, pageNumber, newText);
        }

        /// <summary>
        /// Adds a web link annotation to the specified page.
        /// </summary>
        /// <param name="pageNumber">1‑based page index where the link will be placed.</param>
        /// <param name="rect">Rectangle defining the clickable area (coordinates are in points).</param>
        /// <param name="url">Target URL.</param>
        public void AddWebLink(int pageNumber, Aspose.Pdf.Rectangle rect, string url)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL must be provided.", nameof(url));
            if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page numbers are 1‑based.");

            // PdfContentEditor.CreateWebLink expects a System.Drawing.Rectangle.
            // Convert the Aspose.Pdf.Rectangle to System.Drawing.Rectangle (int based).
            var drawingRect = new System.Drawing.Rectangle(
                (int)rect.LLX,
                (int)rect.LLY,
                (int)(rect.URX - rect.LLX),
                (int)(rect.URY - rect.LLY));

            _editor.CreateWebLink(drawingRect, url, pageNumber);
        }

        /// <summary>
        /// Deletes all images from the document.
        /// </summary>
        public void DeleteAllImages()
        {
            // DeleteImage without parameters removes all images from the whole document.
            _editor.DeleteImage();
        }

        /// <summary>
        /// Saves the edited PDF to the specified path.
        /// </summary>
        /// <param name="outputPath">Destination file path.</param>
        public void Save(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // PdfContentEditor.Save(string) writes the PDF to the given file.
            _editor.Save(outputPath);
        }

        /// <summary>
        /// Releases resources used by the underlying PdfContentEditor.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                // Close releases the bound document and any internal resources.
                _editor?.Close();
                _disposed = true;
            }
        }
    }

    // A minimal entry point is required for the project to compile.
    // It does not perform any operation; it merely satisfies the compiler.
    internal static class Program
    {
        public static void Main()
        {
            // No‑op. The wrapper can be used from other projects or unit tests.
        }
    }
}
