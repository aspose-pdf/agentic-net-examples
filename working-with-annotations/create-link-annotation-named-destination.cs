using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // ---------- Page 1 ----------
            Page page1 = doc.Pages.Add();

            // Add some visible text
            page1.Paragraphs.Add(new TextFragment("Click the blue rectangle to jump to Page 2"));

            // Define the rectangle area for the link annotation
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page1, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue // optional visual cue
            };

            // The action refers to a named destination that will be defined later
            link.Action = new GoToAction(doc, "MyDest");

            // Add the annotation to the page
            page1.Annotations.Add(link);

            // ---------- Page 2 (target) ----------
            Page page2 = doc.Pages.Add();
            page2.Paragraphs.Add(new TextFragment("You have arrived at the named destination."));

            // Register a named destination that points to page2 (fit the whole page)
            // In modern Aspose.PDF the NamedDestinations collection accepts a name and an IAppointment (destination) directly.
            doc.NamedDestinations.Add("MyDest", new FitExplicitDestination(page2));

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}
