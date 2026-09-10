using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (disposal handled by using)
        using (Document doc = new Document(inputPath))
        {
            // Get the last page (1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Margin from the page edges
            double margin = 20;

            // Create a text fragment
            TextFragment fragment = new TextFragment("Rotated Text");

            // Position the fragment at the bottom‑right corner (baseline point)
            fragment.Position = new Position(lastPage.PageInfo.Width - margin, margin);

            // Rotate the text (angle in degrees)
            fragment.TextState.Rotation = 45;

            // Set text appearance via TextState
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(lastPage);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added and saved to '{outputPath}'.");
    }
}
