using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class PdfInMemoryTextExtractor
{
    // Extracts text from a PDF provided as a MemoryStream and returns it in another MemoryStream.
    public static MemoryStream ExtractTextFromPdf(MemoryStream pdfInputStream)
    {
        if (pdfInputStream == null)
            throw new ArgumentNullException(nameof(pdfInputStream));

        // Ensure the input stream is positioned at the beginning.
        pdfInputStream.Position = 0;

        // Output stream that will hold the extracted text.
        MemoryStream textOutputStream = new MemoryStream();

        // PdfExtractor implements IDisposable, so use a using block for deterministic cleanup.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document from the input stream.
            extractor.BindPdf(pdfInputStream);

            // Perform the text extraction operation.
            extractor.ExtractText();

            // Save the extracted text into the output stream.
            extractor.GetText(textOutputStream);
        }

        // Reset the output stream position so it can be read from the beginning by the caller.
        textOutputStream.Position = 0;
        return textOutputStream;
    }

    // Example usage.
    static void Main()
    {
        // Create a simple PDF in memory so the example is self‑contained.
        using (MemoryStream pdfStream = new MemoryStream())
        {
            Document doc = new Document();
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello Aspose PDF!"));
            doc.Save(pdfStream);
            // Rewind the stream before passing it to the extractor.
            pdfStream.Position = 0;

            // Extract text into a new MemoryStream.
            using (MemoryStream resultStream = ExtractTextFromPdf(pdfStream))
            using (StreamReader reader = new StreamReader(resultStream))
            {
                string extractedText = reader.ReadToEnd();
                Console.WriteLine("Extracted Text:");
                Console.WriteLine(extractedText);
            }
        }
    }
}