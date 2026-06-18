using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Load the PDF with a facade that supports modifications.
        // PdfPageEditor allows editing pages and then saving to a stream.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Example modification: add a text stamp to every page.
            TextStamp textStamp = new TextStamp("Modified")
            {
                Background = false,
                Opacity = 0.5f,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Set TextState properties via the read‑only TextState instance.
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Apply the stamp to all pages.
            foreach (Page page in editor.Document.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Export the modified PDF to a memory stream.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                editor.Save(memoryStream);          // SaveableFacade.Save(Stream)
                memoryStream.Position = 0;          // Reset for downstream processing

                // The memory stream now contains the modified PDF.
                Console.WriteLine($"Modified PDF size in memory: {memoryStream.Length} bytes");
                // Further processing can be performed here, e.g., sending the stream to another service.
            }
        }
    }
}
