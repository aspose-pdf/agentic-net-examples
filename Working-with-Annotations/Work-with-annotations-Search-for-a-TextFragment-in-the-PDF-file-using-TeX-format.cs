using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Regular expression to match TeX inline math expressions, e.g., $x+y=1$
        string texPattern = @"\$(.+?)\$";

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that uses regular‑expression search
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(
                texPattern,
                new TextSearchOptions(true)   // enable regex mode
            );

            // Search the whole document
            doc.Pages.Accept(absorber);

            // Add a visual annotation for each found TeX fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The fragment knows the page it belongs to
                Page page = doc.Pages[fragment.Page.Number];

                // Use the fragment's rectangle to position the annotation
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create a semi‑transparent yellow square annotation
                SquareAnnotation annotation = new SquareAnnotation(page, rect)
                {
                    Color   = Aspose.Pdf.Color.Yellow,
                    Opacity = 0.3,
                    Title   = "TeX expression",
                    Contents = fragment.Text
                };

                // Attach the annotation to the page
                page.Annotations.Add(annotation);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}