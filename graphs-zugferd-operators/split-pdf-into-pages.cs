using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF with up to 4 pages (evaluation mode limit)
        using (Document sampleDoc = new Document())
        {
            for (int i = 1; i <= 4; i++)
            {
                Page page = sampleDoc.Pages.Add();
                TextFragment tf = new TextFragment("This is page " + i);
                page.Paragraphs.Add(tf);
            }
            sampleDoc.Save("input.pdf");
        }

        // Load the created PDF
        using (Document srcDoc = new Document("input.pdf"))
        {
            int pageCount = srcDoc.Pages.Count;
            for (int i = 1; i <= pageCount; i++)
            {
                using (Document singlePageDoc = new Document())
                {
                    // Add the current page to a new document
                    singlePageDoc.Pages.Add(srcDoc.Pages[i]);
                    string outputFile = "page_" + i + ".pdf";
                    singlePageDoc.Save(outputFile);
                }
            }
        }
    }
}