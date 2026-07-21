using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextFragment, TextState, HorizontalAlignment

class Program
{
    static void Main()
    {
        const string outputPath = "centered_text.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("This text is centered on the page.");

            // Configure the TextState to use center alignment
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Optionally set font and size for better visibility
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 14;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the fragment (the rectangle defines the area where the text will be placed)
            // Here we use the whole page width; Y coordinate is set to roughly the middle of the page.
            fragment.Position = new Position(0, page.PageInfo.Height / 2);

            // Add the fragment to the page's paragraphs collection
            page.Paragraphs.Add(fragment);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}