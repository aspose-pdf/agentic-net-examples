using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class ExtraAnnotationsDemo
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_annotations.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Add a TextAnnotation (sticky note) with a title and contents
            // ------------------------------------------------------------
            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);
            TextAnnotation textAnn = new TextAnnotation(page, textRect)
            {
                Title    = "Note",
                Contents = "This is a custom text annotation added via Aspose.Pdf.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,                     // Show the popup open by default
                Icon     = TextIcon.Note               // Use the standard note icon
            };
            page.Annotations.Add(textAnn);

            // ------------------------------------------------------------
            // 2. Add a LinkAnnotation that opens an external URL
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(250, 700, 450, 750);
            LinkAnnotation linkAnn = new LinkAnnotation(page, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue,
                // Use GoToURIAction for external hyperlinks (Hyperlink property is not a string)
                Action = new GoToURIAction("https://www.example.com")
            };
            page.Annotations.Add(linkAnn);

            // ------------------------------------------------------------
            // 3. Add a SquareAnnotation (highlighted area) with a border
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SquareAnnotation squareAnn = new SquareAnnotation(page, squareRect);
            squareAnn.Color = Aspose.Pdf.Color.LightGray;
            // Border requires a Border object that takes the parent annotation as a parameter
            squareAnn.Border = new Border(squareAnn) { Width = 2 };
            page.Annotations.Add(squareAnn);

            // ------------------------------------------------------------
            // 4. Add a FreeTextAnnotation with custom appearance
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle freeTextRect = new Aspose.Pdf.Rectangle(100, 400, 400, 460);
            // DefaultAppearance constructor expects System.Drawing.Color for the text color
            DefaultAppearance da = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            FreeTextAnnotation freeTextAnn = new FreeTextAnnotation(page, freeTextRect, da)
            {
                Contents = "Free‑text annotation with custom font and color.",
                Color    = Aspose.Pdf.Color.LightBlue
            };
            // Set the border explicitly (optional)
            freeTextAnn.Border = new Border(freeTextAnn) { Width = 1 };
            page.Annotations.Add(freeTextAnn);

            // ------------------------------------------------------------
            // Save the modified document
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}
