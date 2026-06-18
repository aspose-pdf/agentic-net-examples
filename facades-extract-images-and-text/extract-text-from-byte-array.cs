using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            doc.Pages.Add();
            // Add some text to the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment("Hello, Aspose PDF!");
            page.Paragraphs.Add(fragment);
            // Save the PDF to a temporary file (required for self‑contained example)
            doc.Save("input.pdf");
        }

        // Load the PDF into a byte array
        byte[] pdfBytes = File.ReadAllBytes("input.pdf");

        // Extract text from the PDF byte array without writing to disk
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfStream);
                extractor.ExtractText();

                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    string extractedText = Encoding.UTF8.GetString(textStream.ToArray());
                    Console.WriteLine("Extracted Text:");
                    Console.WriteLine(extractedText);
                }
            }
        }
    }
}