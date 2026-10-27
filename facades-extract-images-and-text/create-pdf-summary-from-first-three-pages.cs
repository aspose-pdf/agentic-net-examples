using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "summary.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Extract text from the first three pages using PdfExtractor (Facades)
        // -----------------------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPath);
        extractor.StartPage = 1;   // 1‑based page index
        extractor.EndPage   = 3;   // inclusive
        extractor.ExtractText();

        // Get the extracted text via a stream (PdfExtractor has no parameter‑less GetText)
        string extractedText;
        using (MemoryStream textStream = new MemoryStream())
        {
            extractor.GetText(textStream); // write text to the stream
            textStream.Position = 0;       // rewind for reading
            using (StreamReader reader = new StreamReader(textStream))
            {
                extractedText = reader.ReadToEnd();
            }
        }

        // ---------------------------------------------------------------
        // Create a new PDF document and add the extracted text as a page
        // ---------------------------------------------------------------
        using (Document summaryDoc = new Document())
        {
            // Add a blank page to the document
            Page page = summaryDoc.Pages.Add();

            // Create a TextFragment with the extracted text
            TextFragment fragment = new TextFragment(extractedText)
            {
                // Optional formatting
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial") }
            };

            // Add the text fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the summary PDF
            summaryDoc.Save(outputPath);
        }

        Console.WriteLine($"Summary PDF created: {outputPath}");
    }
}
