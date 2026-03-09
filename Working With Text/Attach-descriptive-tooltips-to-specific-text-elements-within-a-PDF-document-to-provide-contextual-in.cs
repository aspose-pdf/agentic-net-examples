using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_tooltips.pdf";
        const string keyword    = "ImportantTerm";          // text to annotate
        const string tooltip    = "This term is defined in the glossary.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Search for the target text on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Determine the rectangle that bounds the fragment
                // TextFragment.Rectangle returns an Aspose.Pdf.Rectangle
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Slightly enlarge the rectangle so the annotation icon does not overlap the text
                double padding = 5;
                Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(
                    rect.LLX - padding,
                    rect.LLY - padding,
                    rect.URX + padding,
                    rect.URY + padding);

                // Create a TextAnnotation (sticky note) on the page where the fragment resides
                TextAnnotation annotation = new TextAnnotation(fragment.Page, annotRect)
                {
                    Title    = "Tooltip",               // shown in the annotation title bar
                    Contents = tooltip,                 // the tooltip text
                    Open     = false,                   // annotation is closed by default
                    Icon     = TextIcon.Comment         // choose an appropriate icon
                };

                // Add the annotation to the page's annotation collection
                fragment.Page.Annotations.Add(annotation);
            }

            // Save the modified PDF (using the Save(string) overload)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltips: {outputPath}");
    }
}