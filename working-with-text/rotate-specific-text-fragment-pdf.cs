using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Prepare a sample PDF that contains the text we want to rotate.
        //    This makes the example self‑contained and eliminates the
        //    FileNotFoundException that occurred when "input.pdf" was missing.
        // ---------------------------------------------------------------------
        const string targetText = "Sample Text";
        string tempInputPath = Path.Combine(Path.GetTempPath(), "temp_input.pdf");
        string outputPath   = Path.Combine(Path.GetTempPath(), "rotated_output.pdf");

        // Create a simple PDF with one page and the target text.
        using (Document creator = new Document())
        {
            Page page = creator.Pages.Add();
            TextFragment tf = new TextFragment(targetText);
            // Optional: set a readable font size so the rotation is visible.
            tf.TextState.FontSize = 24;
            page.Paragraphs.Add(tf);
            creator.Save(tempInputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Load the PDF, locate the text fragment and rotate it by 45 degrees.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(tempInputPath))
        {
            // Search for the target text on the first page.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(targetText);
            doc.Pages[1].Accept(absorber);

            // If the text fragment is found, rotate the first occurrence.
            if (absorber.TextFragments.Count > 0)
            {
                // TextFragments collection is 1‑based.
                absorber.TextFragments[1].TextState.Rotation = 45;
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
