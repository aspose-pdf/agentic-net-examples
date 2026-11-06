using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the source PDF into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Use the high‑level Document API (PdfContentEditor does not expose AddText)
            Document pdf = new Document(inputStream);

            // Create a TextFragment with the desired text and styling
            TextFragment tf = new TextFragment("Sample annotation")
            {
                // Position specifies the lower‑left corner of the text box
                Position = new Position(100, 700)
            };
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to page 1 (pages are 1‑based)
            pdf.Pages[1].Paragraphs.Add(tf);

            // Save the modified PDF to the output memory stream (no disk I/O)
            pdf.Save(outputStream);
            outputStream.Position = 0; // reset for downstream consumers

            Console.WriteLine($"Modified PDF size: {outputStream.Length} bytes");
            // outputStream now contains the PDF data and can be returned, sent over network, etc.
        }
    }
}
