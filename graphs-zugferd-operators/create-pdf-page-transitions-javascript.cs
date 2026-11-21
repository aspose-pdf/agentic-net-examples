using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction lives here
using Aspose.Pdf.Text; // TextFragment lives here

class Program
{
    static void Main()
    {
        const string outputPath = "PageTransitions.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add three pages to demonstrate different transitions.
            for (int i = 1; i <= 3; i++)
            {
                // Add a new blank page.
                Page page = doc.Pages.Add();

                // Add simple text to identify the page.
                page.Paragraphs.Add(new TextFragment($"Page {i}"));

                // Define transition style and duration per page.
                // Transition styles: Fade, Dissolve, Split, Blinds, Box, Wipe, etc.
                // Duration is in seconds.
                string style;
                double duration;

                switch (i)
                {
                    case 1:
                        style = "Fade";
                        duration = 2.0;
                        break;
                    case 2:
                        style = "Wipe";
                        duration = 3.0;
                        break;
                    default:
                        style = "Box";
                        duration = 1.5;
                        break;
                }

                // Attach a JavaScript action that runs when the page is opened.
                // The PDF JavaScript method this.setPageTransition(style, duration) sets the transition.
                string js = $"this.setPageTransition('{style}', {duration});";
                // Correct property name (OnOpen) and class name (JavascriptAction).
                page.Actions.OnOpen = new JavascriptAction(js);
            }

            // Save the PDF. The document is saved inside the using block so the object stays alive until Save completes.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}
