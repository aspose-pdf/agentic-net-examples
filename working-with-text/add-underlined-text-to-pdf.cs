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
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Underlined text example");

            // Set the position where the text will appear on the page
            fragment.Position = new Position(100, 700); // X=100, Y=700

            // Enable underlining via the TextState property
            fragment.TextState.Underline = true;

            // Optionally set other visual properties (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Black;

            // Append the fragment to the page using TextBuilder (or page.Paragraphs.Add)
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the PDF to a file
            doc.Save("underlined_text.pdf");
        }

        Console.WriteLine("PDF with underlined text created successfully.");
    }
}