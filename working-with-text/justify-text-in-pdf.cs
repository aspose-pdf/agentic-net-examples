using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "justified.pdf";

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a new page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Sample paragraph text
            string sampleText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                                "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

            // Create a TextFragment and set justification
            TextFragment fragment = new TextFragment(sampleText);
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Justify;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save as PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}