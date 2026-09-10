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

        // Create the facade and bind the source PDF
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(inputPath);

            // Example modification (optional)
            // viewer.Zoom = 0.75f;

            // Export the (modified) PDF to a memory stream
            using (MemoryStream pdfStream = new MemoryStream())
            {
                viewer.Save(pdfStream);          // Save to stream
                pdfStream.Position = 0;          // Reset for downstream use

                Console.WriteLine($"Exported PDF size: {pdfStream.Length} bytes");
                // pdfStream can now be passed to other parts of the application
            }
        }
        finally
        {
            // Release resources held by the facade
            viewer.Close();
        }
    }
}