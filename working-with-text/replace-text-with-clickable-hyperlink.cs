using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string phrase = "click here";                 // phrase to replace
        const string linkUrl = "https://www.example.com";   // target URL
        const string linkText = "Visit Example";            // text that will appear as hyperlink

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for the phrase in the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            doc.Pages.Accept(absorber);

            // Replace each occurrence with a clickable hyperlink
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Change the visible text to the desired link text
                fragment.Text = linkText;

                // Create a LinkAnnotation that covers the fragment's rectangle
                // The fragment already knows the page it belongs to
                Page page = fragment.Page;
                Rectangle rect = fragment.Rectangle;
                LinkAnnotation linkAnnot = new LinkAnnotation(page, rect);

                // Assign a GoToURIAction so the annotation is clickable
                // GoToURIAction resides in Aspose.Pdf.Annotations in recent versions
                linkAnnot.Action = new Aspose.Pdf.Annotations.GoToURIAction(linkUrl);
                // Optional: tooltip that appears when the mouse hovers over the link
                linkAnnot.Contents = linkText;

                // Add the annotation to the page
                page.Annotations.Add(linkAnnot);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}
