using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path for the output PDF file
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document (Pages are 1‑based)
            doc.Pages.Add();

            // Get the first page
            Page page = doc.Pages[1];

            // Define the HTML fragment with styled markup
            // Example: bold and italic text with a colored span
            string htmlContent = "<b>Bold Text</b> <i>Italic Text</i> " +
                                 "<span style=\"color:#FF0000;\">Red Text</span>";

            // Create the HtmlFragment instance
            HtmlFragment htmlFragment = new HtmlFragment(htmlContent);

            // Initialise a TextState object before customizing it (prevents NullReferenceException)
            htmlFragment.TextState = new TextState();

            // Optional: customize the default text state (font, size, color)
            // Resolve the font safely – fall back to a built‑in font if the requested one is missing
            var font = FontRepository.FindFont("Helvetica") ?? FontRepository.FindFont("Arial");
            if (font != null)
            {
                htmlFragment.TextState.Font = font;
            }
            htmlFragment.TextState.FontSize = 12;
            htmlFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the HtmlFragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with HTML fragment saved to '{outputPath}'.");
    }
}
