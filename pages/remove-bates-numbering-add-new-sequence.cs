using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
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

                // Remove any existing stamp annotations (Bates numbers are added as stamps)
                // Iterate backwards because we are deleting items from the collection.
                for (int j = page.Annotations.Count; j >= 1; j--)
                {
                    if (page.Annotations[j] is StampAnnotation)
                    {
                        page.Annotations.Delete(j);
                    }
                }

                // Create a new Bates number stamp for this page
                TextStamp batesStamp = new TextStamp(i.ToString())
                {
                    // Position the stamp at the bottom‑right corner
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Offsets from the page edges
                    RightMargin = 10,   // points from the right edge (when Right‑aligned)
                    BottomMargin = 10   // points from the bottom edge (when Bottom‑aligned)
                };

                // Define visual appearance – TextState is read‑only, so modify its members directly
                batesStamp.TextState.Font = FontRepository.FindFont("Arial");
                batesStamp.TextState.FontSize = 12;
                batesStamp.TextState.ForegroundColor = Color.Black;

                // Add the new stamp to the page
                page.AddStamp(batesStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering updated and saved to '{outputPath}'.");
    }
}
