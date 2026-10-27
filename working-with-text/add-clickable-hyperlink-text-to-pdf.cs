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
        const string outputPath = "output.pdf";
        const string url        = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a text fragment that will contain the clickable text
            TextFragment fragment = new TextFragment("Visit Example");

            // Optional visual styling for the clickable text
            fragment.TextState.Underline = true;
            fragment.TextState.FontSize   = 12;
            fragment.TextState.Font       = FontRepository.FindFont("Arial");

            // Position the fragment on the page (adjust as needed)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page's paragraph collection first – this will render the text
            page.Paragraphs.Add(fragment);

            // Create a link annotation that covers the same rectangle as the text fragment
            // The rectangle coordinates are (llx, lly, urx, ury) – here we approximate the size
            double llx = fragment.Position.XIndent;
            double lly = fragment.Position.YIndent;
            double urx = llx + fragment.TextState.FontSize * fragment.Text.Length * 0.6; // rough width estimate
            double ury = lly + fragment.TextState.FontSize;
            LinkAnnotation link = new LinkAnnotation(page, new Rectangle(llx, lly, urx, ury))
            {
                Action = new GoToURIAction(url)
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clickable text added and saved to '{outputPath}'.");
    }
}
