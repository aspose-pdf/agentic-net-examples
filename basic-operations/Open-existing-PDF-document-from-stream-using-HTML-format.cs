using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // HtmlSaveOptions is also in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path – the source PDF will be opened from a stream.
        const string inputPdfPath  = "input.pdf";

        // Output HTML file path – the result of the conversion.
        const string outputHtmlPath = "output.html";

        // Verify that the source file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF from a FileStream (any Stream works – e.g., MemoryStream, NetworkStream).
        // The Document constructor that accepts a Stream is the recommended lifecycle method.
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        using (Document pdfDoc = new Document(pdfStream))
        {
            // HTML conversion requires GDI+; on non‑Windows platforms this may throw
            // TypeInitializationException. Wrap the save operation in a try‑catch block.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Example option: embed all resources into a single HTML file.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };

            try
            {
                // Save the document as HTML using explicit HtmlSaveOptions.
                pdfDoc.Save(outputHtmlPath, htmlOptions);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
            }
            catch (TypeInitializationException)
            {
                // GDI+ is unavailable (e.g., on macOS/Linux). Inform the user.
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
            }
        }
    }
}