using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "page_transitions.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Define a common page size (A4 in points)
            double pageWidth = 595; // 8.27 inches * 72
            double pageHeight = 842; // 11.69 inches * 72

            // Add pages with distinct transition effects
            AddPageWithTransition(doc, pageWidth, pageHeight, "First Page", "Fade", 2);
            AddPageWithTransition(doc, pageWidth, pageHeight, "Second Page", "Split", 3);
            AddPageWithTransition(doc, pageWidth, pageHeight, "Third Page", "Blinds", 1.5);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to add a page, some content, and a JavaScript Open action that sets the transition
    static void AddPageWithTransition(Document doc, double width, double height,
                                      string pageText, string transitionStyle, double duration)
    {
        // Create a new page with the specified size
        Page page = doc.Pages.Add();
        page.SetPageSize(width, height);

        // Add a simple text fragment so the page is not empty
        TextFragment tf = new TextFragment(pageText);
        tf.Position = new Position(100, height - 100); // place near top-left
        page.Paragraphs.Add(tf);

        // JavaScript that sets the page transition when the page is opened
        // PDF JavaScript API: this.setPageTransition({type: 'Fade', duration: 2});
        string js = $"this.setPageTransition({{type: '{transitionStyle}', duration: {duration}}});";

        // Attach the JavaScript to the page's Open action (correct property is OnOpen)
        page.Actions.OnOpen = new JavascriptAction(js);
    }
}
