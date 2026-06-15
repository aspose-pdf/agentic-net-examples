using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

namespace SetPdfExpiration
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file (self‑contained example)
            using (Document createDoc = new Document())
            {
                Page page = createDoc.Pages.Add();
                TextFragment fragment = new TextFragment("Sample PDF for expiration demo.");
                page.Paragraphs.Add(fragment);
                createDoc.Save("input.pdf");
            }

            // Open the PDF and embed JavaScript to enforce expiration
            using (Document doc = new Document("input.pdf"))
            {
                string jsCode = "Date exp = new Date('2025-12-31'); if (new Date() > exp) { app.alert('Document has expired.'); this.closeDoc(); }";
                JavascriptAction jsAction = new JavascriptAction(jsCode);
                doc.OpenAction = jsAction;
                doc.Save("output.pdf");
            }
        }
    }
}
