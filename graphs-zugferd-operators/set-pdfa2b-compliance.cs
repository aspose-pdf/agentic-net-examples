using System;
using Aspose.Pdf;

namespace SetPdfA2bComplianceExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document createDoc = new Document())
            {
                // Add a page (1‑based indexing)
                Page page = createDoc.Pages.Add();
                // Add some text
                Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment("Hello PDF/A-2b!");
                page.Paragraphs.Add(fragment);
                // Save the temporary file
                createDoc.Save("input.pdf");
            }

            // Load the PDF and convert it to PDF/A‑2b compliance
            using (Document loadDoc = new Document("input.pdf"))
            {
                // Convert the document to PDF/A‑2b. The conversion is performed in‑place.
                // A conversion log file is generated (optional, can be an empty string if not needed).
                loadDoc.Convert("conversion_log.xml", PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);
                // Save the compliant PDF
                loadDoc.Save("output.pdf");
            }
        }
    }
}
