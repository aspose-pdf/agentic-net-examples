using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_figure.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Add a Figure Annotation (SquareAnnotation) to the page
            // ------------------------------------------------------------
            // Define the rectangle where the square will appear
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the square annotation first
            SquareAnnotation square = new SquareAnnotation(page, squareRect);

            // Set its visual properties
            square.Color         = Aspose.Pdf.Color.Blue;          // Border color
            square.InteriorColor = Aspose.Pdf.Color.LightGray;     // Fill color
            square.Contents      = "Figure annotation example";   // Tooltip text
            square.Title         = "Square Figure";                // Title shown in the popup
            square.Opacity       = 0.5;                            // Semi‑transparent
            // Border must be assigned after the object is created
            square.Border        = new Border(square) { Width = 2 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // ------------------------------------------------------------
            // 2. Create a TeX fragment (for demonstration purposes)
            // ------------------------------------------------------------
            // The TeX string we want to keep
            string texString = @"\\frac{a}{b} = c";
            // Instantiate the TeXFragment – this shows the API usage.
            TeXFragment texFragment = new TeXFragment(texString, removeIndents: true);

            // Optionally, embed the TeX string into a text annotation to illustrate how TeX content might be stored.
            Aspose.Pdf.Rectangle texRect = new Aspose.Pdf.Rectangle(320, 500, 520, 700);
            TextAnnotation texAnn = new TextAnnotation(page, texRect)
            {
                Title    = "TeX Formula",
                Contents = texString,               // Store the raw TeX code as the annotation's contents
                Icon     = TextIcon.Note,
                Color    = Aspose.Pdf.Color.Green
            };
            page.Annotations.Add(texAnn);

            // ------------------------------------------------------------
            // Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with figure and TeX annotations saved to '{outputPdf}'.");
    }
}
