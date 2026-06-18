using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    /// <summary>
    /// Simplifies common PDF editing tasks by encapsulating Aspose.Pdf.Facades.PdfContentEditor.
    /// </summary>
    public class PdfEditorWrapper : IDisposable
    {
        private readonly PdfContentEditor _editor;
        private bool _isBound;

        /// <summary>
        /// Initializes the wrapper and binds the specified PDF file for editing.
        /// </summary>
        /// <param name="inputPdfPath">Path to the PDF file to edit.</param>
        public PdfEditorWrapper(string inputPdfPath)
        {
            if (string.IsNullOrWhiteSpace(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found.", inputPdfPath);

            // Create the facade (rule: use provided constructor)
            _editor = new PdfContentEditor();

            // Bind the PDF file (rule: use BindPdf(string))
            _editor.BindPdf(inputPdfPath);
            _isBound = true;
        }

        /// <summary>
        /// Replaces all occurrences of <paramref name="searchText"/> with <paramref name="replaceText"/>.
        /// </summary>
        public void ReplaceAllText(string searchText, string replaceText)
        {
            EnsureBound();
            // Directly use the ReplaceText method of PdfContentEditor
            _editor.ReplaceText(searchText, replaceText);
        }

        /// <summary>
        /// Replaces text on a specific page.
        /// </summary>
        public void ReplaceTextOnPage(int pageNumber, string searchText, string replaceText)
        {
            EnsureBound();
            // Overload that specifies the page number
            _editor.ReplaceText(searchText, pageNumber, replaceText);
        }

        /// <summary>
        /// Adds a file attachment to the PDF without an annotation.
        /// </summary>
        /// <param name="attachmentPath">Path to the file to attach.</param>
        /// <param name="description">Description of the attachment.</param>
        public void AddAttachment(string attachmentPath, string description)
        {
            EnsureBound();

            if (string.IsNullOrWhiteSpace(attachmentPath))
                throw new ArgumentException("Attachment path must be provided.", nameof(attachmentPath));

            if (!File.Exists(attachmentPath))
                throw new FileNotFoundException("Attachment file not found.", attachmentPath);

            // Use the overload that accepts a file path and description
            _editor.AddDocumentAttachment(attachmentPath, description);
        }

        /// <summary>
        /// Adds a custom document-level action (e.g., JavaScript) to the PDF.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="actionScript">Script or command to execute.</param>
        public void AddDocumentAction(string actionName, string actionScript)
        {
            EnsureBound();
            _editor.AddDocumentAdditionalAction(actionName, actionScript);
        }

        /// <summary>
        /// Saves the edited PDF to the specified output path.
        /// </summary>
        /// <param name="outputPdfPath">Path where the edited PDF will be saved.</param>
        public void Save(string outputPdfPath)
        {
            EnsureBound();

            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            // Save using the facade's Save(string) method (rule: use provided save method)
            _editor.Save(outputPdfPath);
        }

        /// <summary>
        /// Releases resources used by the facade.
        /// </summary>
        public void Dispose()
        {
            _editor?.Close();
        }

        private void EnsureBound()
        {
            if (!_isBound)
                throw new InvalidOperationException("PdfContentEditor is not bound to a document.");
        }
    }

    /// <summary>
    /// Simple entry point required for a console‑type project.
    /// Demonstrates basic usage of <see cref="PdfEditorWrapper"/>.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Expect at least input and output PDF paths.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfUtilities <inputPdfPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                using (var editor = new PdfEditorWrapper(inputPath))
                {
                    // Example operation – replace a placeholder token.
                    editor.ReplaceAllText("{PLACEHOLDER}", "Replaced Text");
                    editor.Save(outputPath);
                }

                Console.WriteLine($"PDF edited successfully and saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
