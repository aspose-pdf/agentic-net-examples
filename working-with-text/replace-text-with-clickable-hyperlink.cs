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
        const string replacementText = "Visit Site";
        const string hyperlinkUrl = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the target phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            doc.Pages.Accept(absorber);

            // Replace each occurrence and add a clickable hyperlink
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Replace visible text
                fragment.Text = replacementText;

                // Create a link annotation that covers the fragment rectangle
                Page page = fragment.Page;
                var rect = fragment.Rectangle;
                var link = new LinkAnnotation(page, rect);
                link.Action = new GoToURIAction(hyperlinkUrl);
                link.Contents = replacementText; // optional tooltip

                // Add the annotation to the page
                page.Annotations.Add(link);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
