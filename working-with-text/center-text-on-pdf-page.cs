using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "centered.pdf";

        // Ensure there is a PDF to work with; create a blank one if missing
        if (!File.Exists(inputPath))
        {
            using (Document blankDoc = new Document())
            {
                blankDoc.Pages.Add(); // add a single page
                blankDoc.Save(inputPath);
            }
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            Page page = doc.Pages[1];

            // Create a text fragment
            TextFragment fragment = new TextFragment("Centered Text on the Page");

            // Configure the TextState
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center; // center alignment

            // Set vertical position (X is ignored when centered)
            fragment.Position = new Position(0, 500);

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the result
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}