using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // TextFragment, FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Extract text from the first three pages using PdfExtractor (Facades API)
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.StartPage = 1; // 1‑based indexing
            extractor.EndPage = 3;
            extractor.ExtractText();

            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                extractedText = Encoding.UTF8.GetString(textStream.ToArray());
            }
        }

        // Create a new PDF document that contains the extracted text
        using (Document summaryDoc = new Document())
        {
            // Add a single page to hold the summary
            Page page = summaryDoc.Pages.Add();

            // Create a TextFragment with the extracted text
            TextFragment fragment = new TextFragment(extractedText);
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the summary PDF
            summaryDoc.Save(outputPath);
        }

        Console.WriteLine($"Summary PDF created at '{outputPath}'.");
    }
}