using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "PageTransitions.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add three pages with simple identifying text
            for (int i = 1; i <= 3; i++)
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment($"Page {i}"));
            }

            // JavaScript that defines a transition for each page.
            // The method setPageTransition(pageIndex, style, duration) uses
            // zero‑based page indexes, style names like "Dissolve", "Wipe", "Split",
            // and duration in seconds.
            string js = @"
function setTransitions()
{
    this.setPageTransition(0, 'Dissolve', 2); // Page 1: dissolve, 2 seconds
    this.setPageTransition(1, 'Wipe', 3);    // Page 2: wipe, 3 seconds
    this.setPageTransition(2, 'Split', 1);   // Page 3: split, 1 second
}
setTransitions();
";

            // Attach the JavaScript to the document; it runs when the PDF is opened
            doc.OpenAction = new JavascriptAction(js);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}
