using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add some visible content so the page is not empty
            TextFragment tf = new TextFragment("Sample text");
            page.Paragraphs.Add(tf);
            doc.Save("input.pdf");
        }

        // Open the PDF and delete specific operators from the first page's content stream
        using (Document doc = new Document("input.pdf"))
        {
            OperatorCollection ops = doc.Pages[1].Contents;
            // Example: delete the first operator (operators are 1‑based)
            ops.Delete(1);
            doc.Save("output.pdf");
        }
    }
}