using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Remove any existing stamp annotations from the page
                page.Annotations.Clear();

                // Create a new Bates number stamp for this page
                TextStamp batesStamp = new TextStamp($"Bates-{i:D5}")
                {
                    // Position the stamp at the bottom‑right corner
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Use XIndent/YIndent to provide a margin from the page edges
                    XIndent = 20, // distance from the right edge (since Right alignment)
                    YIndent = 20  // distance from the bottom edge
                };

                // Configure text appearance using Aspose.Pdf.Text classes
                batesStamp.TextState.FontSize = 12;
                batesStamp.TextState.Font = FontRepository.FindFont("Arial");
                batesStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the stamp to the current page
                page.AddStamp(batesStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied and saved to '{outputPath}'.");
    }
}
