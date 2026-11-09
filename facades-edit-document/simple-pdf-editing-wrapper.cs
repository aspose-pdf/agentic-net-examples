using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // kept for potential future use, but not required for current implementation
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

namespace PdfEditingWrapper
{
    /// <summary>
    /// Simplifies common PDF editing tasks using the high‑level Aspose.Pdf Document API.
    /// The older PdfContentEditor methods such as InsertPage have been removed from recent
    /// Aspose.Pdf releases; this wrapper implements equivalent functionality with Document.
    /// </summary>
    public class PdfEditorWrapper : IDisposable
    {
        private readonly Document _doc;
        private bool _disposed = false;

        /// <summary>
        /// Loads the PDF file to be edited.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF.</param>
        public PdfEditorWrapper(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // Load the document – this is the object we will manipulate.
            _doc = new Document(pdfPath);
        }

        /// <summary>
        /// Inserts a page from another PDF at the specified position.
        /// </summary>
        /// <param name="sourcePdfPath">Path to the PDF containing the page to insert.</param>
        /// <param name="sourcePageNumber">1‑based page number in the source PDF.</param>
        /// <param name="insertAtPageNumber">1‑based page number in the target PDF where the page will be inserted.</param>
        public void InsertPage(string sourcePdfPath, int sourcePageNumber, int insertAtPageNumber)
        {
            if (!File.Exists(sourcePdfPath))
                throw new FileNotFoundException("Source PDF not found.", sourcePdfPath);

            // Load the source document, extract the required page and insert it.
            using (Document srcDoc = new Document(sourcePdfPath))
            {
                if (sourcePageNumber < 1 || sourcePageNumber > srcDoc.Pages.Count)
                    throw new ArgumentOutOfRangeException(nameof(sourcePageNumber), "Source page number is out of range.");

                // Aspose.Pdf pages collection is 1‑based.
                Page srcPage = srcDoc.Pages[sourcePageNumber];
                // Insert creates a copy of the page inside the target document.
                _doc.Pages.Insert(insertAtPageNumber, srcPage);
            }
        }

        /// <summary>
        /// Deletes a page from the PDF.
        /// </summary>
        /// <param name="pageNumber">1‑based page number to delete.</param>
        public void DeletePage(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > _doc.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            _doc.Pages.Delete(pageNumber);
        }

        /// <summary>
        /// Moves a page to a new position within the PDF.
        /// </summary>
        /// <param name="pageNumber">1‑based page number to move.</param>
        /// <param name="newPosition">1‑based target position.</param>
        public void MovePage(int pageNumber, int newPosition)
        {
            if (pageNumber < 1 || pageNumber > _doc.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");
            if (newPosition < 1 || newPosition > _doc.Pages.Count + 1)
                throw new ArgumentOutOfRangeException(nameof(newPosition), "New position is out of range.");

            // Extract the page, delete it, then re‑insert at the new position.
            Page page = _doc.Pages[pageNumber];
            _doc.Pages.Delete(pageNumber);
            // If the page was removed from a position before the new position, the index shifts left by one.
            int adjustedPosition = newPosition > pageNumber ? newPosition - 1 : newPosition;
            _doc.Pages.Insert(adjustedPosition, page);
        }

        /// <summary>
        /// Adds an image stamp to a specific page.
        /// </summary>
        /// <param name="imagePath">Path to the image file.</param>
        /// <param name="pageNumber">1‑based page number where the stamp will be placed.</param>
        /// <param name="llx">Lower‑left X coordinate (points).</param>
        /// <param name="lly">Lower‑left Y coordinate (points).</param>
        /// <param name="urx">Upper‑right X coordinate (points).</param>
        /// <param name="ury">Upper‑right Y coordinate (points).</param>
        public void AddImageStamp(string imagePath, int pageNumber, double llx, double lly, double urx, double ury)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);

            if (pageNumber < 1 || pageNumber > _doc.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            ImageStamp stamp = new ImageStamp(imagePath)
            {
                // Position using XIndent/YIndent (Aspose.Pdf uses these instead of Left/Bottom).
                XIndent = (float)llx,
                YIndent = (float)lly,
                Width = (float)(urx - llx),
                Height = (float)(ury - lly),
                // Optional alignment – keep default (Top‑Left) which works with the explicit indents.
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Background = false
            };

            _doc.Pages[pageNumber].AddStamp(stamp);
        }

        /// <summary>
        /// Adds a text annotation (visible as a stamp) to a specific page.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <param name="pageNumber">1‑based page number where the text will be placed.</param>
        /// <param name="llx">Lower‑left X coordinate (points).</param>
        /// <param name="lly">Lower‑left Y coordinate (points).</param>
        /// <param name="urx">Upper‑right X coordinate (points).</param>
        /// <param name="ury">Upper‑right Y coordinate (points).</param>
        public void AddTextStamp(string text, int pageNumber, double llx, double lly, double urx, double ury)
        {
            if (pageNumber < 1 || pageNumber > _doc.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            // Create a TextFragment and position its lower‑left corner.
            TextFragment tf = new TextFragment(text)
            {
                Position = new Position(llx, lly)
            };

            // Optional styling – callers can modify the TextState after this call if needed.
            // tf.TextState.Font = FontRepository.FindFont("Helvetica");
            // tf.TextState.FontSize = 12;
            // tf.TextState.ForegroundColor = Color.Black;

            _doc.Pages[pageNumber].Paragraphs.Add(tf);
        }

        /// <summary>
        /// Saves the edited PDF to the specified path.
        /// </summary>
        /// <param name="outputPath">Destination file path.</param>
        public void Save(string outputPath)
        {
            // Ensure the directory exists.
            string dir = System.IO.Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            _doc.Save(outputPath);
        }

        /// <summary>
        /// Exposes the current page count – useful for callers that need to validate page numbers.
        /// </summary>
        public int PageCount => _doc?.Pages?.Count ?? 0;

        /// <summary>
        /// Releases resources used by the wrapper.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _doc?.Dispose();
                _disposed = true;
            }
        }
    }

    // Example usage of the wrapper.
    class Program
    {
        static void Main()
        {
            const string sourcePdf = "original.pdf";
            const string insertPdf = "insert_page.pdf";
            const string outputPdf = "edited.pdf";

            // Ensure example files exist before running.
            if (!File.Exists(sourcePdf) || !File.Exists(insertPdf))
            {
                Console.Error.WriteLine("Required PDF files are missing.");
                return;
            }

            // Use the wrapper inside a using block for deterministic disposal.
            using (PdfEditorWrapper editor = new PdfEditorWrapper(sourcePdf))
            {
                // Insert page 1 of insertPdf after page 2 of the source PDF.
                editor.InsertPage(insertPdf, 1, 3);

                // Delete page 5 (if it exists).
                if (5 <= editor.PageCount)
                    editor.DeletePage(5);

                // Move page 4 to position 2.
                editor.MovePage(4, 2);

                // Add an image stamp to page 1.
                editor.AddImageStamp("logo.png", 1, 50, 750, 150, 800);

                // Add a text stamp to page 2.
                editor.AddTextStamp("Confidential", 2, 200, 500, 400, 550);

                // Save the modified PDF.
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Edited PDF saved to '{outputPdf}'.");
        }
    }

    // The previous reflection‑based extension method has been removed in favour of the public PageCount property.
}
