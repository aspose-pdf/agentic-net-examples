using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "justified.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Sample text to be justified
            string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                          "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            // Create a TextFragment with the sample text
            TextFragment fragment = new TextFragment(text);

            // Set horizontal alignment to Justify during creation
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Justify;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}