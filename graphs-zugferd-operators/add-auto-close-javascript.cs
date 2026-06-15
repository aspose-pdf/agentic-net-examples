using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF file
        using (Document sampleDoc = new Document())
        {
            Page page = sampleDoc.Pages.Add();
            TextFragment tf = new TextFragment("Sample PDF for JavaScript auto‑close.");
            page.Paragraphs.Add(tf);
            sampleDoc.Save("input.pdf");
        }

        // Open the sample PDF and add JavaScript that closes the document after 5 seconds
        using (Document doc = new Document("input.pdf"))
        {
            JavascriptAction jsAction = new JavascriptAction("app.setTimeOut('this.closeDoc();', 5000);");
            doc.OpenAction = jsAction;
            doc.Save("output.pdf");
        }
    }
}