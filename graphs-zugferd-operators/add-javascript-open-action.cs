using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a single page (evaluation mode limit)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the PDF and assign a JavaScript open action that jumps to page five
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // PDF JavaScript uses zero‑based page numbers, so page five is index 4
            string jsCode = "this.pageNum = 4;";
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            pdfDoc.OpenAction = jsAction;
            pdfDoc.Save("output.pdf");
        }
    }
}