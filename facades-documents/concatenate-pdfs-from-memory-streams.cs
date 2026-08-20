using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create two sample PDFs completely in memory.
        byte[] pdfBytes1 = CreateSamplePdf("First PDF", "This is the first document.");
        byte[] pdfBytes2 = CreateSamplePdf("Second PDF", "This is the second document.");

        // Wrap the byte arrays in memory streams – these are the source streams.
        using (MemoryStream sourceStream1 = new MemoryStream(pdfBytes1))
        using (MemoryStream sourceStream2 = new MemoryStream(pdfBytes2))
        // Destination stream writes directly to the output file without intermediate storage.
        using (FileStream outputStream = new FileStream("merged_output.pdf", FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor implements the concatenation functionality.
            PdfFileEditor editor = new PdfFileEditor
            {
                // Close the source streams automatically after concatenation.
                CloseConcatenatedStreams = true
            };

            // Concatenate the two input streams into the output stream.
            editor.Concatenate(new Stream[] { sourceStream1, sourceStream2 }, outputStream);
        }

        Console.WriteLine("PDF files have been concatenated to 'merged_output.pdf'.");
    }

    // Helper that creates a minimal PDF, writes it to a MemoryStream and returns the byte array.
    private static byte[] CreateSamplePdf(string title, string body)
    {
        using (var doc = new Document())
        {
            var page = doc.Pages.Add();

            // Title paragraph
            var titleFragment = new TextFragment(title)
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold, ForegroundColor = Color.Blue }
            };
            page.Paragraphs.Add(titleFragment);

            // Body paragraph
            var bodyFragment = new TextFragment(body)
            {
                TextState = { FontSize = 12, ForegroundColor = Color.Black }
            };
            page.Paragraphs.Add(bodyFragment);

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }
}