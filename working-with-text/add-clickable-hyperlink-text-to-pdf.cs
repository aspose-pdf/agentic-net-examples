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
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment that will contain the clickable text
            TextFragment fragment = new TextFragment("Click here to visit Example.com");
            // Position the fragment on the page (coordinates are in points)
            fragment.Position = new Position(100, 700);
            // Optional visual style
            fragment.TextState.FontSize = 14;
            fragment.TextState.FontStyle = FontStyles.Bold;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the fragment to the page's paragraphs collection first so that its rectangle is calculated
            page.Paragraphs.Add(fragment);

            // Create a link annotation that covers the same rectangle as the text fragment
            // The TextFragment.Rectangle is populated after the fragment is added to the page
            LinkAnnotation link = new LinkAnnotation(page, fragment.Rectangle);
            // Assign a URI action (GoToURIAction) to open the web address
            link.Action = new GoToURIAction(url);
            // Optionally set the visible tooltip (Contents) for the annotation
            link.Contents = "Open example.com";

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clickable text added and saved to '{outputPath}'.");
    }
}
