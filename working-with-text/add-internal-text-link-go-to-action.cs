using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // LinkAnnotation, GoToAction, Border, Rectangle
using Aspose.Pdf.Text;          // TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Choose the target page (1‑based index). Here we navigate to page 3.
            Page targetPage = doc.Pages[3];

            // Create a visible text fragment that will act as the hyperlink
            TextFragment linkFragment = new TextFragment("Go to Page 3");
            linkFragment.Position = new Position(100, 700); // place on the page
            linkFragment.TextState.FontSize = 14;
            linkFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            linkFragment.TextState.Underline = true; // typical hyperlink style

            // Add the fragment to the first page (or any page you prefer)
            doc.Pages[1].Paragraphs.Add(linkFragment);

            // Determine the rectangle that covers the text fragment.
            // Approximate width using font size; for precise layout you could use TextFragment.Rectangle.
            double rectLeft = 100;
            double rectBottom = 700 - linkFragment.TextState.FontSize; // baseline adjustment
            double rectRight = rectLeft + (linkFragment.TextState.FontSize * linkFragment.Text.Length * 0.5);
            double rectTop = 700;
            // Use the fully‑qualified Aspose.Pdf.Rectangle (annotation bounds type)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(rectLeft, rectBottom, rectRight, rectTop);

            // Create a LinkAnnotation and assign a GoToAction pointing to the target page
            // The constructor requires the owning page and the rectangle.
            LinkAnnotation linkAnnotation = new LinkAnnotation(doc.Pages[1], linkRect);
            linkAnnotation.Action = new GoToAction(targetPage);
            // Make the annotation invisible (no border, no background)
            linkAnnotation.Color = Aspose.Pdf.Color.Transparent;
            linkAnnotation.Border = new Border(linkAnnotation) { Width = 0 };

            // Add the annotation to the same page
            doc.Pages[1].Annotations.Add(linkAnnotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal text link saved to '{outputPath}'.");
    }
}
