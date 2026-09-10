using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfMemoryProcessor
{
    /// <summary>
    /// Loads a PDF from <paramref name="inputPdfStream"/>, adds the specified <paramref name="text"/>
    /// to the first page, and writes the resulting PDF to <paramref name="outputPdfStream"/>.
    /// </summary>
    public static void AddTextToPdf(Stream inputPdfStream, string text, Stream outputPdfStream)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
        if (outputPdfStream == null) throw new ArgumentNullException(nameof(outputPdfStream));
        if (text == null) throw new ArgumentNullException(nameof(text));

        // Load the PDF from the input stream. Document implements IDisposable, so wrap it in using.
        using (Document doc = new Document(inputPdfStream))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content.
            TextFragment tf = new TextFragment(text);
            // Position the text on the page (coordinates are in points).
            tf.Position = new Position(100, 700);
            // Optional styling.
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Append the text fragment to the page.
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified document to the output stream.
            doc.Save(outputPdfStream);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Entry point required for compilation. No operation performed here.
        // Example usage can be added if needed.
    }
}
