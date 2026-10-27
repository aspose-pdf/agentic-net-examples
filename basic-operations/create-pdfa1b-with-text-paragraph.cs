using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath = "conversion_log.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Hello, PDF/A‑1b world!");

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Convert the document to PDF/A‑1b compliance
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}