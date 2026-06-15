using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF file
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Load the PDF and add a document‑level JavaScript action that expires the document
        using (Document doc = new Document("input.pdf"))
        {
            string javascript = "Date expiry = new Date('2025-12-31'); if (new Date() > expiry) { this.closeDoc(); }";
            JavascriptAction jsAction = new JavascriptAction(javascript);
            doc.OpenAction = jsAction;
            doc.Save("output.pdf");
        }
    }
}