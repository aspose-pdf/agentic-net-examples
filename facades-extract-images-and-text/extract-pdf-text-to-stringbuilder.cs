using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf);
        }

        // StringBuilder to accumulate extracted text.
        StringBuilder sb = new StringBuilder();

        // Extract text using PdfExtractor and keep everything in memory.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractText();

            using (MemoryStream ms = new MemoryStream())
            {
                // Use the stream overload to avoid temporary files.
                extractor.GetText(ms);
                string extracted = Encoding.Unicode.GetString(ms.ToArray());
                sb.Append(extracted);
            }
        }

        // Write the accumulated text to disk using Unicode encoding.
        File.WriteAllText(outputTxt, sb.ToString(), Encoding.Unicode);
    }

    // Helper that creates a very simple PDF when the expected input file is missing.
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF for text extraction."));
            doc.Save(path);
        }
    }
}
