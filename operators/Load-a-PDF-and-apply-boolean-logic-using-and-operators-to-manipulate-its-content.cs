using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Example boolean logic:
                //   isEvenPage  – true if the page number is even
                //   hasText     – true if the page contains at least one paragraph
                //   condition   – true only when the page is even AND it has text
                bool isEvenPage = (pageIndex % 2) == 0;
                bool hasText    = page.Paragraphs.Count > 0;
                bool condition  = isEvenPage && hasText;          // && operator
                bool opposite   = !condition;                     // ! operator
                bool either     = isEvenPage || !hasText;         // || operator

                // If the condition is true, add a visible text annotation
                if (condition)
                {
                    // Fully qualified rectangle to avoid ambiguity with System.Drawing
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                    TextAnnotation annotation = new TextAnnotation(page, rect)
                    {
                        Title    = $"Page {pageIndex}",
                        Contents = "Even page with text content",
                        Open     = true,
                        Icon     = TextIcon.Note,
                        Color    = Aspose.Pdf.Color.Yellow
                    };
                    page.Annotations.Add(annotation);
                }
                // If the opposite condition is true, add a simple stamp annotation
                else if (opposite)
                {
                    // Create a stamp annotation (using ImageStamp for illustration)
                    // Note: ImageStamp is part of Aspose.Pdf.Facades, but we can use a simple TextAnnotation here
                    Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(50, 750, 150, 800);
                    TextAnnotation stamp = new TextAnnotation(page, stampRect)
                    {
                        Title    = $"Page {pageIndex}",
                        Contents = "Odd page or no text",
                        Open     = false,
                        Icon     = TextIcon.Comment,
                        Color    = Aspose.Pdf.Color.LightGray
                    };
                    page.Annotations.Add(stamp);
                }

                // Demonstrate the use of the || operator: if either condition holds, add a footer text fragment
                if (either)
                {
                    TextFragment footer = new TextFragment($"Footer on page {pageIndex}");
                    footer.Position = new Position(50, 20); // Position from bottom-left
                    footer.TextState.FontSize = 10;
                    footer.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                    page.Paragraphs.Add(footer);
                }
            }

            // Save the modified document (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}