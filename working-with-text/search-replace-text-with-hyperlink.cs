using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;               // TextFragmentAbsorber, TextFragment
using Aspose.Pdf.Annotations;        // LinkAnnotation, GoToURIAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchPhrase = "Click here";
        const string replacementText = "Visit Aspose";
        const string url = "https://www.aspose.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for the phrase on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);
            }

            // Replace each found fragment and attach a clickable hyperlink
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Change the visible text
                fragment.Text = replacementText;

                // Create a link annotation that covers the fragment's rectangle
                // The fragment already knows the page it belongs to via fragment.Page
                LinkAnnotation link = new LinkAnnotation(fragment.Page, fragment.Rectangle);
                // Use GoToURIAction (canonical class name) for a clickable URL
                link.Action = new GoToURIAction(url);
                link.Contents = url;                // optional tooltip

                // Add the annotation to the page
                fragment.Page.Annotations.Add(link);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}
