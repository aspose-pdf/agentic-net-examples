using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment(
                "The quick brown fox jumps over the lazy dog. " +
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");

            // Configure the existing TextState (read‑only property) – do NOT replace the object
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Justify;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Black;

            // Position the fragment on the page (left margin = 50, bottom margin = 500)
            fragment.Position = new Position(50, 500);

            // Add the TextFragment to the page's paragraphs collection
            page.Paragraphs.Add(fragment);

            // Save the document as PDF
            doc.Save("JustifiedText.pdf");
        }

        Console.WriteLine("PDF with justified text saved as 'JustifiedText.pdf'.");
    }
}