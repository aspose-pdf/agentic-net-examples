using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

namespace PdfEditingWrapper
{
    /// <summary>
    /// Simplified wrapper around Aspose.Pdf.Facades.PdfContentEditor.
    /// Provides common editing operations without exposing the full facade API.
    /// </summary>
    public sealed class PdfEditorWrapper : IDisposable
    {
        private readonly PdfContentEditor _editor;
        private bool _isBound;

        /// <summary>
        /// Initializes a new instance of the wrapper.
        /// </summary>
        public PdfEditorWrapper()
        {
            // Create the underlying PdfContentEditor (rule: use provided constructor)
            _editor = new PdfContentEditor();
            _isBound = false;
        }

        /// <summary>
        /// Binds a PDF file for editing.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        public void Load(string inputPdfPath)
        {
            if (string.IsNullOrWhiteSpace(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found.", inputPdfPath);

            // Bind the PDF file (rule: use BindPdf(string))
            _editor.BindPdf(inputPdfPath);
            _isBound = true;
        }

        /// <summary>
        /// Replaces all occurrences of a text string in the whole document.
        /// </summary>
        /// <param name="searchText">Text to search for.</param>
        /// <param name="replaceText">Replacement text.</param>
        public void ReplaceAllText(string searchText, string replaceText)
        {
            EnsureBound();
            // ReplaceText(string, string) replaces text throughout the document
            _editor.ReplaceText(searchText, replaceText);
        }

        /// <summary>
        /// Replaces text on a specific page.
        /// </summary>
        /// <param name="pageNumber">1‑based page index.</param>
        /// <param name="searchText">Text to search for.</param>
        /// <param name="replaceText">Replacement text.</param>
        public void ReplaceTextOnPage(int pageNumber, string searchText, string replaceText)
        {
            EnsureBound();
            // ReplaceText(string, int, string) replaces text on the given page
            _editor.ReplaceText(searchText, pageNumber, replaceText);
        }

        /// <summary>
        /// Adds a file attachment to the PDF (no visual annotation).
        /// </summary>
        /// <param name="attachmentPath">Path to the file to attach.</param>
        /// <param name="description">Optional description for the attachment.</param>
        public void AddAttachment(string attachmentPath, string description = "")
        {
            EnsureBound();

            if (string.IsNullOrWhiteSpace(attachmentPath))
                throw new ArgumentException("Attachment path must be provided.", nameof(attachmentPath));

            if (!File.Exists(attachmentPath))
                throw new FileNotFoundException("Attachment file not found.", attachmentPath);

            // AddDocumentAttachment(string, string) adds an attachment without annotation
            _editor.AddDocumentAttachment(attachmentPath, description);
        }

        /// <summary>
        /// Saves the edited PDF to the specified output path.
        /// </summary>
        /// <param name="outputPdfPath">Destination file path.</param>
        public void Save(string outputPdfPath)
        {
            EnsureBound();

            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            // Save the modified PDF (rule: use Save(string))
            _editor.Save(outputPdfPath);
        }

        /// <summary>
        /// Ensures that a PDF has been bound before performing operations.
        /// </summary>
        private void EnsureBound()
        {
            if (!_isBound)
                throw new InvalidOperationException("No PDF is bound. Call Load() before performing edit operations.");
        }

        /// <summary>
        /// Releases all resources used by the wrapper.
        /// </summary>
        public void Dispose()
        {
            _editor?.Close(); // Close releases the bound document
            _editor?.Dispose();
        }
    }

    // Example usage (self‑contained; creates required files at runtime)
    class Program
    {
        static void Main()
        {
            const string inputPath = "sample.pdf";
            const string outputPath = "sample_edited.pdf";
            const string attachmentPath = "notes.txt";

            // ------------------------------------------------------------
            // 1️⃣ Create a minimal source PDF so the wrapper has something to load.
            // ------------------------------------------------------------
            using (Document seed = new Document())
            {
                // Add a single page with some searchable text.
                Page page = seed.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Hello world! This is page 1."));
                // Add a second page to demonstrate page‑specific replacement.
                Page page2 = seed.Pages.Add();
                page2.Paragraphs.Add(new TextFragment("Page2Old content on page 2."));
                seed.Save(inputPath);
            }

            // ------------------------------------------------------------
            // 2️⃣ Create a dummy attachment file.
            // ------------------------------------------------------------
            File.WriteAllText(attachmentPath, "Sample notes attached to the PDF.");

            // ------------------------------------------------------------
            // 3️⃣ Perform editing via the wrapper.
            // ------------------------------------------------------------
            using (PdfEditorWrapper wrapper = new PdfEditorWrapper())
            {
                wrapper.Load(inputPath);
                wrapper.ReplaceAllText("Hello", "Hi");
                wrapper.ReplaceTextOnPage(2, "Page2Old", "Page2New");
                wrapper.AddAttachment(attachmentPath, "Additional notes");
                wrapper.Save(outputPath);
            }

            Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
        }
    }
}
