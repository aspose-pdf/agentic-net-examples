using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // LinkAnnotation, GoToURIAction are here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string phraseToFind = "Click here";
        const string replacementText = "Visit Aspose";
        const string url = "https://www.aspose.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Search for the target phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phraseToFind);
            doc.Pages.Accept(absorber);

            // Replace each occurrence and add a clickable hyperlink
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Change the displayed text
                fragment.Text = replacementText;

                // Create a link annotation that covers the fragment's rectangle
                Page page = fragment.Page;
                var rect = fragment.Rectangle;
                var link = new LinkAnnotation(page, rect)
                {
                    // Optional tooltip (what appears on hover)
                    Contents = replacementText,
                    // Define the action that opens the URL
                    Action = new GoToURIAction(url)
                };

                // Add the annotation to the page
                page.Annotations.Add(link);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}
