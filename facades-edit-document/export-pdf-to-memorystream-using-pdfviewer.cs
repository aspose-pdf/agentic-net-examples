using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with a facade (PdfViewer) – this follows the required lifecycle rules.
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(inputPath);

            // Example modification placeholder – PdfViewer does not expose a Zoom property.
            // Apply any required changes here using the appropriate facade methods.
            // For instance, you could use PdfPageEditor for page‑level transformations.

            // Export the resulting PDF to a memory stream for further processing.
            using (MemoryStream memory = new MemoryStream())
            {
                // SaveableFacade.Save(Stream) writes the PDF into the provided stream.
                viewer.Save(memory);

                // Reset the stream position before any downstream consumer reads it.
                memory.Position = 0;

                Console.WriteLine($"PDF exported to memory stream, length = {memory.Length} bytes");
                // The 'memory' stream can now be passed to other components or APIs.
            }
        }
        finally
        {
            // Ensure the facade releases all resources.
            viewer.Close();
        }
    }
}
