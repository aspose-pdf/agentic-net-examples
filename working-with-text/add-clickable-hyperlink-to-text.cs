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
        const string phrase = "Click here";
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Search for the phrase in the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            absorber.Visit(doc);

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Keep the original visible text (or change it if desired)
                fragment.Text = phrase;

                // Create a link annotation that covers the fragment's rectangle
                // and assign a GoToURIAction to make it clickable.
                LinkAnnotation link = new LinkAnnotation(fragment.Page, fragment.Rectangle);
                link.Action = new GoToURIAction(url);
                // Make the annotation invisible (no border, transparent colour)
                link.Color = Color.Transparent;
                link.Border = new Border(link) { Width = 0 };

                // Add the annotation to the page
                fragment.Page.Annotations.Add(link);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}
