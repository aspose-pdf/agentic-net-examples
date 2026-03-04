using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facades for page manipulation
using Aspose.Pdf.Text;               // Required for EpubSaveOptions (inherits UnifiedSaveOptions)

// Input PDF and output EPUB paths
const string inputPdfPath  = "input.pdf";
const string outputEpubPath = "output.epub";

if (!File.Exists(inputPdfPath))
{
    Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
    return;
}

// Load the PDF document, manipulate pages with PdfPageEditor, then save as EPUB
using (Document pdfDoc = new Document(inputPdfPath))
{
    // ------------------------------------------------------------
    // Page manipulation using the Facades API (PdfPageEditor)
    // ------------------------------------------------------------
    // Bind the document to the editor. The editor implements IDisposable,
    // so we wrap it in a using block to ensure proper resource release.
    using (PdfPageEditor pageEditor = new PdfPageEditor(pdfDoc))
    {
        // Example operation: rotate every page 90 degrees clockwise.
        // The Rotation property applies to all pages unless ProcessPages is set.
        pageEditor.Rotation = 90;

        // Optionally, change the zoom factor (1.0 = 100%). Here we set 0.8 (80%).
        pageEditor.Zoom = 0.8f;

        // Apply the changes to the underlying document.
        pageEditor.ApplyChanges();
    }

    // ------------------------------------------------------------
    // Convert the manipulated PDF to EPUB format
    // ------------------------------------------------------------
    // Configure EPUB save options. The ContentRecognitionMode enum is
    // nested inside EpubSaveOptions, so we reference it as shown.
    EpubSaveOptions epubOptions = new EpubSaveOptions
    {
        ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
        // Additional options (e.g., Title) can be set here if needed.
    };

    // Save the document as an EPUB file using the explicit SaveOptions.
    pdfDoc.Save(outputEpubPath, epubOptions);
}

Console.WriteLine($"EPUB file created successfully at '{outputEpubPath}'.");