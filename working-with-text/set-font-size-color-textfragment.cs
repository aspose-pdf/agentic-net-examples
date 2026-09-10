using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "styled_output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");

            // Set font size and color via the fragment's TextState
            fragment.TextState.FontSize = 24;                     // Font size
            fragment.TextState.ForegroundColor = Color.Red;      // Text color (Aspose.Pdf.Color)

            // Optionally set the font family (e.g., Helvetica)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");

            // Add the styled TextFragment to the page's paragraphs collection
            page.Paragraphs.Add(fragment);

            // Save the document (PDF format) using the standard Save method
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}