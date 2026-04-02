using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF with three pages
        using (Document sampleDoc = new Document())
        {
            // Add three blank pages
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add page numbers
        using (Document doc = new Document("input.pdf"))
        {
            // Iterate through all pages
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                PageNumberStamp pageNumberStamp = new PageNumberStamp();
                pageNumberStamp.StartingNumber = 1;
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
                page.AddStamp(pageNumberStamp);
            }

            doc.Save("output.pdf");
        }
    }
}