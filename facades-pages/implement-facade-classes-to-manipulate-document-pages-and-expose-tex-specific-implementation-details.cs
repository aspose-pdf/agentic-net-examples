using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfAdvancedProcessing
{
    // Facade wrapper that provides high‑level page manipulation
    // and TeX export capabilities using Aspose.Pdf.Facades.
    public sealed class PdfPageProcessor : IDisposable
    {
        private readonly PdfPageEditor _editor;
        private bool _disposed;
        private readonly string _originalPath;

        // Constructor binds the PDF file to the PdfPageEditor facade.
        public PdfPageProcessor(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            _originalPath = pdfPath;
            _editor = new PdfPageEditor();
            _editor.BindPdf(pdfPath); // lifecycle: bind using provided method
        }

        // Rotates the specified pages by the given angle (0, 90, 180, 270).
        public void RotatePages(int[] pageNumbers, int rotation)
        {
            if (pageNumbers == null || pageNumbers.Length == 0)
                throw new ArgumentException("At least one page number must be supplied.", nameof(pageNumbers));

            if (rotation != 0 && rotation != 90 && rotation != 180 && rotation != 270)
                throw new ArgumentException("Rotation must be 0, 90, 180 or 270 degrees.", nameof(rotation));

            // PageRotations expects a Dictionary<int, int> where key = page number, value = rotation.
            var rotations = new Dictionary<int, int>();
            foreach (int page in pageNumbers)
                rotations[page] = rotation;

            _editor.PageRotations = rotations;
            _editor.ApplyChanges(); // apply the rotation changes
        }

        // Sets a uniform zoom factor for all pages (1.0 = 100%).
        public void SetZoom(float zoomFactor)
        {
            if (zoomFactor <= 0)
                throw new ArgumentOutOfRangeException(nameof(zoomFactor), "Zoom factor must be positive.");

            _editor.Zoom = zoomFactor;
            _editor.ApplyChanges();
        }

        // Changes the output page size (e.g., A4, Letter).
        public void ChangePageSize(PageSize newSize)
        {
            if (newSize == null)
                throw new ArgumentNullException(nameof(newSize));

            _editor.PageSize = newSize;
            _editor.ApplyChanges();
        }

        // Moves the origin of all pages to a new position.
        public void MoveOrigin(float offsetX, float offsetY)
        {
            _editor.MovePosition(offsetX, offsetY);
            _editor.ApplyChanges();
        }

        // Saves the edited document as a TeX file using TeXSaveOptions.
        public void SaveAsTeX(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // The PdfPageEditor facade does not expose a Save overload that accepts SaveOptions.
            // To export to TeX we first save the edited PDF to a temporary file, then load it
            // with the Document class which does support TeXSaveOptions.
            string tempPdf = System.IO.Path.GetTempFileName();
            _editor.Save(tempPdf);

            var doc = new Document(tempPdf);
            var texOptions = new TeXSaveOptions
            {
                CacheGlyphs = true // example TeX‑specific option
            };
            doc.Save(outputPath, texOptions);

            // Clean up the temporary PDF.
            try { System.IO.File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }
        }

        // Exposes the underlying TeXSaveOptions for advanced callers.
        public TeXSaveOptions CreateTeXSaveOptions()
        {
            return new TeXSaveOptions();
        }

        // Proper disposal of the facade and its bound document.
        public void Dispose()
        {
            if (_disposed) return;

            _editor?.Close();   // releases the bound Document
            _editor?.Dispose(); // disposes the facade itself
            _disposed = true;
        }
    }

    // Example usage of the PdfPageProcessor facade.
    class Program
    {
        static void Main()
        {
            const string inputPdf = "sample.pdf";
            const string outputTex = "sample.tex";

            // Ensure the input file exists before processing.
            if (!System.IO.File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                return;
            }

            // Use the processor inside a using block to guarantee disposal.
            using (var processor = new PdfPageProcessor(inputPdf))
            {
                // Rotate pages 2 and 3 by 90 degrees.
                processor.RotatePages(new[] { 2, 3 }, 90);

                // Set zoom to 150% for better readability.
                processor.SetZoom(1.5f);

                // Change the output page size to A4.
                processor.ChangePageSize(PageSize.A4);

                // Move the origin 20 points right and 30 points up.
                processor.MoveOrigin(20f, 30f);

                // Export the result to TeX format.
                processor.SaveAsTeX(outputTex);
            }

            Console.WriteLine($"Processing complete. TeX file saved to '{outputTex}'.");
        }
    }
}
