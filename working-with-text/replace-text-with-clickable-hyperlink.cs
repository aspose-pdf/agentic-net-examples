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
        const string searchPhrase = "Click here";
        const string linkText     = "Visit Site";
        const string url          = "https://example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate all occurrences of the phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            doc.Pages.Accept(absorber);

            // Replace each occurrence with a styled hyperlink and add a LinkAnnotation
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Replace the text
                fragment.Text = linkText;
                // Style the hyperlink (blue and underlined)
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                fragment.TextState.Underline = true;

                // Get the page that contains the fragment
                Page page = fragment.Page; // Correct way to obtain the page

                // Create a link annotation over the same rectangle
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;
                LinkAnnotation link = new LinkAnnotation(page, rect);
                link.Action = new GoToURIAction(url);
                // Make the annotation border invisible
                link.Border = new Border(link) { Width = 0 };
                page.Annotations.Add(link);
            }

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}
