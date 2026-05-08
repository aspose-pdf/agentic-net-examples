using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace PdfMemoryProcessorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example usage (optional):
            // using (var input = new MemoryStream(File.ReadAllBytes("input.pdf")))
            // using (var output = new MemoryStream())
            // {
            //     PdfMemoryProcessor.AddTextToPdf(input, output);
            //     File.WriteAllBytes("output.pdf", output.ToArray());
            // }
        }
    }

    public static class PdfMemoryProcessor
    {
        /// <summary>
        /// Loads a PDF from the input stream, adds a text fragment to the first page,
        /// and writes the modified PDF to the output stream.
        /// </summary>
        /// <param name="inputPdfStream">Stream containing the source PDF (must be readable).</param>
        /// <param name="outputPdfStream">Stream that will receive the resulting PDF (must be writable).</param>
        public static void AddTextToPdf(Stream inputPdfStream, Stream outputPdfStream)
        {
            // Ensure the input stream is at the beginning
            if (inputPdfStream.CanSeek)
                inputPdfStream.Position = 0;

            // Use a using block for deterministic disposal of the Document
            using (Document pdfDoc = new Document(inputPdfStream))
            {
                // Get the first page (Aspose.Pdf uses 1‑based indexing)
                Page firstPage = pdfDoc.Pages[1];

                // Create a text fragment with the desired content
                TextFragment text = new TextFragment("Hello, Aspose.Pdf!");
                // Position the text on the page (coordinates are in points)
                text.Position = new Position(100, 700);

                // Optional styling
                text.TextState.FontSize = 14;
                text.TextState.Font = FontRepository.FindFont("Helvetica");
                text.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

                // Append the text fragment to the page using TextBuilder
                TextBuilder builder = new TextBuilder(firstPage);
                builder.AppendText(text);

                // Save the modified document to the output stream
                pdfDoc.Save(outputPdfStream);
            }

            // Reset the output stream position for callers that will read from it
            if (outputPdfStream.CanSeek)
                outputPdfStream.Position = 0;
        }
    }
}