using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a PDF that will be used as the background stamp
        using (Document backgroundDoc = new Document())
        {
            Page bgPage = backgroundDoc.Pages.Add();
            bgPage.Paragraphs.Add(new TextFragment("Background Page"));
            backgroundDoc.Save("background.pdf");
        }

        // Create the target PDF that will receive the background stamp
        using (Document targetDoc = new Document())
        {
            Page targetPage = targetDoc.Pages.Add();
            targetPage.Paragraphs.Add(new TextFragment("Content Page"));
            targetDoc.Save("input.pdf");
        }

        // Open both PDFs and apply the background stamp
        using (Document targetDoc = new Document("input.pdf"))
        {
            using (Document backgroundDoc = new Document("background.pdf"))
            {
                // Get the page that will be used as the stamp (first page)
                Page stampSourcePage = backgroundDoc.Pages[1];

                // Create a PdfPageStamp from the source page
                PdfPageStamp pageStamp = new PdfPageStamp(stampSourcePage);
                pageStamp.Background = true; // place stamp behind existing content

                // Apply the stamp to each page of the target document
                foreach (Page page in targetDoc.Pages)
                {
                    page.AddStamp(pageStamp);
                }

                targetDoc.Save("output.pdf");
            }
        }
    }
}