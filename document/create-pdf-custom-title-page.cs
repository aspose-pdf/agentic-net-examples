using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string outputPath = "TitlePage.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (this will be the title page)
            Page titlePage = doc.Pages.Add();

            // Create a TextFragment for the title text
            TextFragment title = new TextFragment("My Custom Title");

            // Set custom font – using a built‑in font (Helvetica). 
            // For external TTF fonts use FontRepository.OpenFont("path/to/font.ttf")
            Font font = FontRepository.FindFont("Helvetica");
            title.TextState.Font = font;

            // Set custom font size
            title.TextState.FontSize = 36;

            // Set custom text color (using Aspose.Pdf.Color, not System.Drawing.Color)
            title.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.2, 0.5, 0.8); // Light blue

            // Center the title on the page
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;

            // Add the TextFragment to the page's paragraph collection
            titlePage.Paragraphs.Add(title);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with title page saved to '{outputPath}'.");
    }
}