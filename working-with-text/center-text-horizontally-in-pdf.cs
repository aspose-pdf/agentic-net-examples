using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for HorizontalAlignment enum

class Program
{
    static void Main()
    {
        const string outputPath = "centered.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Centered Text");

            // Configure the existing TextState (font name and size)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 20;

            // Center the text horizontally on the page
            fragment.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}