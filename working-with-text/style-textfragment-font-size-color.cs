using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Styled text using TextState");

            // Set font size and color via the TextState object (read‑only property)
            fragment.TextState.FontSize = 24;                         // font size
            fragment.TextState.ForegroundColor = Color.Blue;          // text color

            // Optionally set a specific font (e.g., Helvetica)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF (Document.Save without SaveOptions always writes PDF)
            doc.Save("styled_output.pdf");
        }

        Console.WriteLine("PDF with styled TextFragment saved as 'styled_output.pdf'.");
    }
}