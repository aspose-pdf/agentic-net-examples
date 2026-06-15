using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add(); // Page 1
            doc.Pages.Add(); // Page 2
            doc.Pages.Add(); // Page 3

            // (Optional) Add simple text to each page for demonstration
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                TextFragment tf = new TextFragment($"Page {i}");
                tf.TextState.FontSize = 24;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Color.Black;
                doc.Pages[i].Paragraphs.Add(tf);
            }

            // Convert the document to PDF/X‑3 compliance.
            // The conversion writes a log file; errors are removed (Delete) during conversion.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted PDF/X‑3 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 file saved to '{outputPath}'.");
    }
}