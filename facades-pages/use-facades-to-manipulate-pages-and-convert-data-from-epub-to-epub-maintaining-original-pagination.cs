using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Facades;            // Facade classes (PdfPageEditor)
using Aspose.Pdf.Text;               // Required for load/save options (if needed)

// This example loads an EPUB file, optionally manipulates its pages using a facade,
// and saves it back to EPUB while preserving the original pagination.
// The pagination is preserved by using the PdfFlow recognition mode
// which keeps the logical order of pages during EPUB conversion.

class Program
{
    static void Main()
    {
        // Input EPUB file (source)
        const string inputEpubPath  = "input.epub";

        // Intermediate PDF file – the EPUB is first converted to PDF.
        // This file is used only as a temporary holder; it can be kept in memory.
        const string tempPdfPath    = "temp.pdf";

        // Output EPUB file (result)
        const string outputEpubPath = "output.epub";

        // Verify that the source file exists
        if (!File.Exists(inputEpubPath))
        {
            Console.Error.WriteLine($"Source EPUB not found: {inputEpubPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the EPUB into a Document using EpubLoadOptions.
            //    The load options can be customized (e.g., page size) if needed.
            // ------------------------------------------------------------
            EpubLoadOptions epubLoadOptions = new EpubLoadOptions(); // default A4 page size
            using (Document doc = new Document(inputEpubPath, epubLoadOptions))
            {
                // ------------------------------------------------------------
                // 2. OPTIONAL PAGE MANIPULATION USING FACADES.
                //    Here we use PdfPageEditor (a facade) to demonstrate page editing.
                //    For example, we set a uniform zoom factor and rotate no pages.
                //    Any other manipulation (margins, rotation, etc.) can be applied
                //    via the PdfPageEditor properties before calling ApplyChanges().
                // ------------------------------------------------------------
                PdfPageEditor pageEditor = new PdfPageEditor(doc);
                pageEditor.Zoom = 1.0f;          // 100% zoom – keep original size (float literal)
                pageEditor.Rotation = 0;       // No rotation
                // If you need to edit only specific pages, set ProcessPages:
                // pageEditor.ProcessPages = new int[] { 1, 2, 3 };
                pageEditor.ApplyChanges();     // Apply the changes to the document

                // ------------------------------------------------------------
                // 3. Save the intermediate PDF to a temporary file.
                //    This step is required because the EPUB save routine works
                //    from a PDF document.
                // ------------------------------------------------------------
                doc.Save(tempPdfPath);
            }

            // ------------------------------------------------------------
            // 4. Load the temporary PDF (produced from the EPUB) back into a Document.
            //    This Document will be the source for the final EPUB conversion.
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document(tempPdfPath))
            {
                // ------------------------------------------------------------
                // 5. Configure EPUB save options.
                //    Use the PdfFlow recognition mode to keep the original pagination.
                // ------------------------------------------------------------
                EpubSaveOptions epubSaveOptions = new EpubSaveOptions
                {
                    // PdfFlow preserves the page order and pagination in the resulting EPUB.
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.PdfFlow
                };

                // ------------------------------------------------------------
                // 6. Save the document as EPUB.
                // ------------------------------------------------------------
                pdfDoc.Save(outputEpubPath, epubSaveOptions);
            }

            // Cleanup the temporary PDF file.
            if (File.Exists(tempPdfPath))
            {
                File.Delete(tempPdfPath);
            }

            Console.WriteLine($"EPUB conversion completed. Output saved to '{outputEpubPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
