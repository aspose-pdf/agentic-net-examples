using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add JavaScript print dialog action
        using (Document doc = new Document("input.pdf"))
        {
            JavascriptAction printAction = new JavascriptAction("this.print(true);");
            doc.OpenAction = printAction;
            doc.Save("output.pdf");
        }
    }
}