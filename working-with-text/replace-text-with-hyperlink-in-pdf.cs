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
        const string searchPhrase = "click here";
        const string replacementText = "Visit Aspose";
        const string url = "https://www.aspose.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber to find the target phrase in the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Perform the search on all pages (Document.Accept was removed in newer versions)
            doc.Pages.Accept(absorber);

            // Iterate over all found fragments and replace them with a hyperlink
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Change the displayed text
                fragment.Text = replacementText;

                // Create a link annotation that covers the fragment's rectangle
                Page page = fragment.Page;
                var rect = fragment.Rectangle;
                var link = new LinkAnnotation(page, rect)
                {
                    Action = new GoToURIAction(url) // GoToURIAction resides in Aspose.Pdf.Annotations
                };

                // Add the annotation to the page so the text becomes clickable
                page.Annotations.Add(link);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}
