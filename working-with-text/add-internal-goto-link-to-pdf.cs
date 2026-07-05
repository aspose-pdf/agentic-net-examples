using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextFragment and LocalHyperlink

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const int targetPageNumber = 2;   // page to navigate to when the link is clicked

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the target page exists; add a blank page if necessary
            while (doc.Pages.Count < targetPageNumber)
                doc.Pages.Add();

            // Create a TextFragment that will act as the clickable link
            TextFragment linkFragment = new TextFragment("Go to page " + targetPageNumber);
            linkFragment.TextState.FontSize = 14;
            linkFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            linkFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Assign a LocalHyperlink (internal GoTo action) to the fragment
            // LocalHyperlink.TargetPageNumber specifies the destination page (1‑based indexing)
            linkFragment.Hyperlink = new LocalHyperlink { TargetPageNumber = targetPageNumber };

            // Add the fragment to the first page (or any page you prefer)
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(linkFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}