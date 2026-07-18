using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextFragment, Position, TextBuilder, FontRepository

class PdfMemoryProcessor
{
    /// <summary>
    /// Loads a PDF from the given input stream, adds a text fragment to the first page,
    /// and returns the modified PDF as a new memory stream.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF (must be readable).</param>
    /// <returns>MemoryStream with the updated PDF.</returns>
    public static MemoryStream AddTextAndReturnStream(Stream inputPdfStream)
    {
        // Ensure the input stream is at the beginning
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Output stream will hold the result
        var outputPdfStream = new MemoryStream();

        // Use a using block for deterministic disposal of the Document
        using (var doc = new Document(inputPdfStream))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
            // Position the text on the page (coordinates are in points)
            tf.Position = new Position(100, 700);

            // Optional: set font, size and colors
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified document into the output stream
            doc.Save(outputPdfStream);
        }

        // Reset the output stream position so it can be read from the beginning
        outputPdfStream.Position = 0;
        return outputPdfStream;
    }
}

class Program
{
    // Entry point required for a console application
    static void Main()
    {
        // Example usage (commented out – replace with real paths if needed)
        // using (var input = File.OpenRead("input.pdf"))
        // {
        //     var resultStream = PdfMemoryProcessor.AddTextAndReturnStream(input);
        //     File.WriteAllBytes("output.pdf", resultStream.ToArray());
        // }
    }
}