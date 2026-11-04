using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input parameters
        const string outputPdfPath = "output_pdfa1b.pdf";
        const string conversionLogPath = "conversion_log.xml";
        const string paragraphText = "This is a sample paragraph on the first page.";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPdfPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document, add a page and a text paragraph
        using (Document doc = new Document())
        {
            // Add a blank page
            doc.Pages.Add();

            // Add a TextFragment to the first page
            TextFragment tf = new TextFragment(paragraphText);
            doc.Pages[1].Paragraphs.Add(tf);

            // Convert the document to PDF/A‑1b
            // The Convert method writes a log file; the log path can be any writable location
            doc.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b compliant document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑1b document saved to '{outputPdfPath}'.");
    }
}