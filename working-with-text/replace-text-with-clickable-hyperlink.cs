using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string phrase     = "Aspose";                     // phrase to search
        const string url        = "https://www.aspose.com";     // hyperlink target

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find all occurrences of the phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            absorber.TextSearchOptions = new TextSearchOptions(true); // case‑insensitive
            doc.Pages.Accept(absorber);

            // Convert each found fragment into a clickable hyperlink
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Visual styling to indicate a link
                fragment.TextState.Underline = true;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

                // Create a link annotation that covers the fragment's rectangle
                // NOTE: LinkAnnotation requires both the owning Page and the rectangle bounds.
                LinkAnnotation link = new LinkAnnotation(fragment.Page, fragment.Rectangle);
                link.Action = new GoToURIAction(url);
                // Add the annotation to the page that contains the fragment
                fragment.Page.Annotations.Add(link);
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}
