using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the sample PDF and attach JavaScript that adds a header and footer on each page load
        using (Document doc = new Document("input.pdf"))
        {
            string jsCode = "this.addWatermarkFromText('Header - Page '+this.pageNum, 'Helvetica', 12, 0xFF0000, 0, 0, 0, 0);" +
                            "this.addWatermarkFromText('Footer - Page '+this.pageNum, 'Helvetica', 12, 0xFF0000, 0, 0, 0, 0, true);";
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            doc.OpenAction = jsAction;
            doc.Save("output.pdf");
        }
    }
}