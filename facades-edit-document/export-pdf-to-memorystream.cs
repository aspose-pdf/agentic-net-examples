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

        // MemoryStream will receive the modified PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Load the PDF with a Facade that supports saving to a stream
            using (PdfViewer viewer = new PdfViewer())
            {
                // Load the source PDF
                viewer.BindPdf(inputPath);

                // Perform any required modifications here (e.g., viewer.Zoom = 0.8f;)

                // Export the result to the memory stream
                viewer.Save(outputStream);
            }

            // Reset the stream position so it can be read later
            outputStream.Position = 0;

            // Example usage: display the size of the exported PDF
            Console.WriteLine($"Exported PDF size: {outputStream.Length} bytes");

            // The outputStream can now be passed to other components for further processing
        }
    }
}