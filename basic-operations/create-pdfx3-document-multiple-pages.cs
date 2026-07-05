using System;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // For optional text fragments (if needed)

class Program
{
    static void Main()
    {
        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add three blank pages (pages are 1‑based)
            doc.Pages.Add();   // Page 1
            doc.Pages.Add();   // Page 2
            doc.Pages.Add();   // Page 3

            // (Optional) Add simple text to each page to illustrate content
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                TextFragment tf = new TextFragment($"Page {i}");
                tf.Position = new Position(100, 700);
                page.Paragraphs.Add(tf);
            }

            // Convert the document to PDF/X‑3 compliance.
            // The conversion log file is optional; here we write it to "conversion_log.xml".
            doc.Convert("conversion_log.xml", PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X‑3 document.
            doc.Save("output_pdfx3.pdf");
        }

        Console.WriteLine("PDF/X‑3 document created: output_pdfx3.pdf");
    }
}