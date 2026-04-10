using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF using a Facade (PdfViewer) – this avoids direct Document usage.
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(inputPdfPath); // Initialize the facade with the source PDF.

            // Perform any required modifications here using the viewer or other facades.
            // Example: set the viewer to editor mode (optional).
            // viewer.FormPresentationMode = FormPresentationMode.Editor;

            // Export the resulting PDF to a memory stream.
            using (MemoryStream pdfStream = new MemoryStream())
            {
                viewer.Save(pdfStream);          // SaveableFacade.Save(Stream) writes PDF to the stream.
                pdfStream.Position = 0;          // Reset stream position for downstream processing.

                // Example usage of the stream (e.g., send to a web service, write to another file, etc.).
                Console.WriteLine($"PDF exported to memory stream, size = {pdfStream.Length} bytes");
                // Further processing can be performed here using pdfStream.
            }
        }
    }
}