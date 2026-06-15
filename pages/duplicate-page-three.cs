using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages (evaluation mode allows up to 4 pages)
        using (Document document = new Document())
        {
            int pageIndex;
            for (pageIndex = 1; pageIndex <= 3; pageIndex++)
            {
                Page page = document.Pages.Add();
                TextFragment text = new TextFragment("Page " + pageIndex);
                page.Paragraphs.Add(text);
            }
            document.Save("input.pdf");
        }

        // Open the PDF and duplicate page three
        using (Document document = new Document("input.pdf"))
        {
            // Extract the textual content of page 3
            Page originalPage = document.Pages[3];
            TextAbsorber absorber = new TextAbsorber();
            originalPage.Accept(absorber);
            string pageText = absorber.Text;

            // Insert a new blank page after page 3 (as page 4)
            Page duplicatedPage = document.Pages.Insert(4);

            // Add the extracted text to the new page
            TextFragment duplicatedText = new TextFragment(pageText);
            duplicatedPage.Paragraphs.Add(duplicatedText);

            document.Save("output.pdf");
        }
    }
}
