using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Simulated queue message containing the PDF file name
        string queueMessage = "sample.pdf";

        // Ensure the sample PDF exists (self‑contained example)
        CreateSamplePdf(queueMessage);

        // Process the PDF: extract its text content
        ExtractTextFromPdf(queueMessage);
    }

    static void CreateSamplePdf(string filePath)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a simple text fragment to the page
            Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment("Hello, Aspose PDF!");
            page.Paragraphs.Add(fragment);
            doc.Save(filePath);
        }
    }

    static void ExtractTextFromPdf(string filePath)
    {
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(filePath);
            extractor.ExtractText();
            string outputPath = "extracted.txt";
            extractor.GetText(outputPath);

            // Read and display the extracted text
            string extracted = File.ReadAllText(outputPath, Encoding.UTF8);
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(extracted);
        }
    }
}