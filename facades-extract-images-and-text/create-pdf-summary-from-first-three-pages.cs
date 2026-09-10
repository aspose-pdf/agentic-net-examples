using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "summary.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Extract text from the first three pages using PdfExtractor (Facades API)
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Limit extraction to pages 1‑3 (1‑based indexing)
            extractor.StartPage = 1;
            extractor.EndPage   = 3;

            // Perform text extraction
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                extractedText = System.Text.Encoding.UTF8.GetString(textStream.ToArray());
            }
        }

        // Create a new PDF document that will hold the summary
        using (Document summaryDoc = new Document())
        {
            // Add a single page to the document
            summaryDoc.Pages.Add();

            // Create a TextFragment with the extracted text
            TextFragment fragment = new TextFragment(extractedText);

            // Configure appearance via the existing TextState instance (read‑only property)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Black;

            // Add the fragment to the first page's paragraphs collection
            summaryDoc.Pages[1].Paragraphs.Add(fragment);

            // Save the summary PDF
            summaryDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Summary PDF created: {outputPdfPath}");
    }
}
