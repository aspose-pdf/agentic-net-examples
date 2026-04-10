using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Simulate a network stream (read/write, seekable)
        using (MemoryStream networkStream = new MemoryStream())
        {
            // Create a simple PDF in memory and write it to the simulated network stream.
            CreateSamplePdf(networkStream);
            // Reset position to the beginning for reading.
            networkStream.Position = 0;

            // Load the PDF from the stream, rotate pages, and write back.
            RotatePdfInStream(networkStream, 90);
        }

        Console.WriteLine("PDF rotation applied and saved to the simulated network stream.");
    }

    private static void CreateSamplePdf(Stream output)
    {
        // Build a one‑page PDF with some text.
        Document doc = new Document();
        Page page = doc.Pages.Add();
        page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Sample PDF"));
        doc.Save(output);
    }

    private static void RotatePdfInStream(Stream networkStream, int angle)
    {
        // The stream must be readable for binding and writable for the result.
        // Use a temporary buffer for the edited PDF.
        using (MemoryStream sourceBuffer = new MemoryStream())
        {
            // Copy the original content to a buffer that can be read multiple times.
            networkStream.CopyTo(sourceBuffer);
            sourceBuffer.Position = 0;

            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(sourceBuffer);
                editor.Rotation = angle; // allowed values: 0, 90, 180, 270

                using (MemoryStream resultBuffer = new MemoryStream())
                {
                    editor.Save(resultBuffer);
                    // Prepare the original network stream for overwriting.
                    networkStream.SetLength(0); // truncate
                    networkStream.Position = 0;
                    resultBuffer.Position = 0;
                    resultBuffer.CopyTo(networkStream);
                    networkStream.Position = 0; // leave stream ready for next read if needed
                }
            }
        }
    }
}