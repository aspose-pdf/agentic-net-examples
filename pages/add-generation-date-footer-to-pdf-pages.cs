using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Prepare the footer text – generation date.
            string footerText = $"Generated on {DateTime.Now:yyyy-MM-dd}";

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment for the footer.
                TextFragment tf = new TextFragment(footerText)
                {
                    // Set text color (cross‑platform Aspose.Pdf.Color).
                    TextState = { ForegroundColor = Aspose.Pdf.Color.Gray },

                    // Position the footer near the bottom of the page.
                    // Coordinates are in points; (0,0) is bottom‑left.
                    // Adjust Y value as needed for margin.
                    Position = new Position(50, 20)
                };

                // Add the fragment to the page's paragraph collection.
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF using the standard Save method.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footer saved to '{outputPath}'.");
    }
}