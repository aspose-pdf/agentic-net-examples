using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace AsposePdfExamples
{
    class JavascriptOpenActionPageTen
    {
        static void Main(string[] args)
        {
            // Create a sample PDF (evaluation mode allows a maximum of 4 pages)
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // Page 1
                doc.Pages.Add(); // Page 2
                doc.Pages.Add(); // Page 3
                doc.Pages.Add(); // Page 4
                doc.Save("input.pdf");
            }

            // Reopen the PDF and set a JavaScript open action to navigate to page ten
            using (Document doc = new Document("input.pdf"))
            {
                // In PDF JavaScript, page numbers are zero‑based. Page ten => index 9.
                JavascriptAction jsAction = new JavascriptAction("this.pageNum = 9;");
                doc.OpenAction = jsAction;
                doc.Save("output.pdf");
            }
        }
    }
}
