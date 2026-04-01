using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages
        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Create a page number stamp with custom prefix "Chapter"
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Chapter #");
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20f;

            // Apply the stamp to each page individually
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                page.AddStamp(pageNumberStamp);
            }

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}