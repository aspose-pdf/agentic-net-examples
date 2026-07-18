using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "lineheight_output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with multiple lines
            TextFragment fragment = new TextFragment("First line\nSecond line\nThird line");

            // Configure the existing TextState (read‑only property) with custom line spacing
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.LineSpacing = 20; // line height in points

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
