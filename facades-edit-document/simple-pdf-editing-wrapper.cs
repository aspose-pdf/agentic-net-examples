using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

namespace PdfEditingWrapper
{
    // Wrapper that simplifies common PDF editing operations using the high‑level Document API.
    public class PdfEditor : IDisposable
    {
        private Document? _document; // Made nullable to satisfy the non‑nullable warning.
        private bool _isLoaded = false;

        public PdfEditor() { }

        // Loads a PDF file into the editor.
        public void Load(string pdfPath)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            _document = new Document(pdfPath);
            _isLoaded = true;
        }

        // Adds plain text to a page using a rectangle for positioning.
        public void AddText(int pageNumber, string text, double llx, double lly, double urx, double ury)
        {
            EnsureLoaded();
            if (pageNumber < 1 || pageNumber > _document!.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            TextFragment fragment = new TextFragment(text)
            {
                // Position is the lower‑left corner of the text.
                Position = new Position(llx, lly)
            };
            // Optional: you can also set the rectangle via fragment.Rectangle if you need clipping.
            _document.Pages[pageNumber].Paragraphs.Add(fragment);
        }

        // Overload that adds text using the default placement (top‑left of the page).
        public void AddText(int pageNumber, string text)
        {
            EnsureLoaded();
            if (pageNumber < 1 || pageNumber > _document!.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            TextFragment fragment = new TextFragment(text);
            _document.Pages[pageNumber].Paragraphs.Add(fragment);
        }

        // Inserts an image onto a page within the specified rectangle.
        // Implemented with ImageStamp because Aspose.Pdf.Image no longer exposes a Rectangle property.
        public void AddImage(int pageNumber, string imagePath, double llx, double lly, double urx, double ury)
        {
            EnsureLoaded();
            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Image file not found: {imagePath}");
            if (pageNumber < 1 || pageNumber > _document!.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            // Use ImageStamp for absolute positioning.
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                // Position the stamp using alignment + indent values.
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                XIndent = (float)llx,
                YIndent = (float)lly,
                // Optionally fix width/height to match the rectangle size.
                // The stamp will be scaled to fit the rectangle defined by the indents.
                // Aspose.Pdf does not provide a direct Rectangle property for ImageStamp.
            };
            _document.Pages[pageNumber].AddStamp(imgStamp);
        }

        // Adds a stamp (e.g., watermark) to a page within the specified rectangle.
        // Implemented with ImageStamp using alignment and margin properties.
        public void AddStamp(int pageNumber, string stampPath, double llx, double lly, double urx, double ury)
        {
            EnsureLoaded();
            if (!File.Exists(stampPath))
                throw new FileNotFoundException($"Stamp file not found: {stampPath}");
            if (pageNumber < 1 || pageNumber > _document!.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            ImageStamp stamp = new ImageStamp(stampPath)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                XIndent = (float)llx,
                YIndent = (float)lly,
                // Margins can be used to fine‑tune placement relative to the rectangle.
                // For example, to keep the stamp inside the rectangle defined by (llx,lly,urx,ury):
                // LeftMargin = (float)llx, BottomMargin = (float)lly, etc.
                // Here we simply use the indents for positioning.
                Background = false
            };
            _document.Pages[pageNumber].AddStamp(stamp);
        }

        // Deletes a page from the PDF.
        public void DeletePage(int pageNumber)
        {
            EnsureLoaded();
            if (pageNumber < 1 || pageNumber > _document!.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            _document.Pages.Delete(pageNumber);
        }

        // Inserts a page from another PDF at the given position.
        public void InsertPage(int pageNumber, string sourcePdfPath)
        {
            EnsureLoaded();
            if (!File.Exists(sourcePdfPath))
                throw new FileNotFoundException($"Source PDF not found: {sourcePdfPath}");
            if (pageNumber < 1 || pageNumber > _document!.Pages.Count + 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            Document sourceDoc = new Document(sourcePdfPath);
            // Insert the first page of the source document at the specified position.
            _document.Pages.Insert(pageNumber, sourceDoc.Pages[1]);
        }

        // Saves the edited PDF to the specified output path.
        public void Save(string outputPath)
        {
            EnsureLoaded();
            _document!.Save(outputPath);
        }

        // Dispose pattern – Document implements IDisposable.
        public void Dispose()
        {
            _document?.Dispose();
        }

        private void EnsureLoaded()
        {
            if (!_isLoaded || _document == null)
                throw new InvalidOperationException("No PDF has been loaded. Call Load() first.");
        }
    }

    // Example usage of the PdfEditor wrapper.
    class Program
    {
        static void Main()
        {
            const string inputPdf = "sample.pdf";
            const string outputPdf = "sample_edited.pdf";
            const string imagePath = "logo.png";
            const string stampPath = "stamp.png";

            try
            {
                using (PdfEditor editor = new PdfEditor())
                {
                    editor.Load(inputPdf);

                    // Add a header text to page 1.
                    editor.AddText(
                        pageNumber: 1,
                        text: "Confidential Document",
                        llx: 50,   // lower‑left X
                        lly: 750,  // lower‑left Y
                        urx: 550,  // upper‑right X (unused for TextFragment positioning)
                        ury: 800   // upper‑right Y (unused for TextFragment positioning)
                    );

                    // Insert an image on page 2.
                    editor.AddImage(
                        pageNumber: 2,
                        imagePath: imagePath,
                        llx: 100,
                        lly: 500,
                        urx: 300,
                        ury: 700
                    );

                    // Apply a stamp on page 3.
                    editor.AddStamp(
                        pageNumber: 3,
                        stampPath: stampPath,
                        llx: 200,
                        lly: 200,
                        urx: 400,
                        ury: 400
                    );

                    // Delete page 4 (if it exists).
                    try { editor.DeletePage(4); } catch { /* ignore if page does not exist */ }

                    // Insert an extra page from another PDF after page 2.
                    editor.InsertPage(3, "extra_page.pdf");

                    // Persist changes.
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
}
