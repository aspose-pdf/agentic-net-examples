using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "subscript_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("H₂O"); // Example: chemical formula

            // Set the subscript rendering by enabling Subscript.
            // Internally this applies a negative rise to the text.
            fragment.TextState.Subscript = true;

            // Optional: set font and size for better appearance
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 24;

            // Position the fragment on the page (baseline at (100, 500))
            fragment.Position = new Position(100, 500);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with subscript text saved to '{outputPath}'.");
    }
}