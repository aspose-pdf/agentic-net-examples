using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF (replace with your actual file)
        const string sourcePdfPath = "source.pdf";

        // Ensure the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"File not found: {sourcePdfPath}");
            return;
        }

        // Use a facade (PdfViewer) to load, optionally modify, and save the PDF to a MemoryStream
        using (PdfViewer viewer = new PdfViewer())
        {
            // Load the PDF from file
            viewer.BindPdf(sourcePdfPath);

            // NOTE: PdfViewer does not expose a Zoom property. If zooming is required,
            // use PdfPageEditor with the Zoom property before rendering, or adjust
            // the viewer's rendering options via other available members.

            // Prepare a memory stream to receive the PDF bytes
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Save the processed PDF directly into the stream (no disk I/O)
                viewer.Save(outputStream);

                // At this point, outputStream contains the PDF data.
                // Reset the position if you need to read from the beginning.
                outputStream.Position = 0;
                byte[] pdfBytes = outputStream.ToArray();

                // Example: write the size of the generated PDF to console
                Console.WriteLine($"PDF saved to MemoryStream, size = {pdfBytes.Length} bytes");
            }
        }
    }
}
