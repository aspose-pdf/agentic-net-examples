using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with three pages (evaluation mode limit is 4 pages)
        using (Document doc = new Document())
        {
            for (int i = 1; i <= 3; i++)
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment("Page " + i);
                tf.TextState.FontSize = 24;
                page.Paragraphs.Add(tf);
            }
            doc.Save("input.pdf");
        }

        // Step 2: Load the PDF and add JavaScript page transition actions to each page
        using (Document doc = new Document("input.pdf"))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // JavaScript sets a Cover transition with a duration of 2 seconds
                string js = "this.setPageTransition({duration:2, transition:'Cover'});";
                page.Actions.OnOpen = new JavascriptAction(js);
            }
            doc.Save("output.pdf");
        }
    }
}