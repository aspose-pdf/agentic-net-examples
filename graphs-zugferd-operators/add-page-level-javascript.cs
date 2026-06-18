using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF with two pages
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Reopen the PDF and add a page‑level JavaScript action
        using (Document doc = new Document("input.pdf"))
        {
            // Get the second page (1‑based indexing)
            Page page = doc.Pages[2];

            // Assign JavaScript that runs when the page is opened (displayed)
            JavascriptAction jsAction = new JavascriptAction("app.alert('You have reached page 2');");
            page.Actions.OnOpen = jsAction;

            // Save the modified document
            doc.Save("output.pdf");
        }
    }
}