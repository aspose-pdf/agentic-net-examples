using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link text will be placed (first page)
            Page page = doc.Pages[1];

            // Create a TextFragment that will act as the visible link text
            TextFragment linkFragment = new TextFragment("Go to Page 3")
            {
                Position = new Position(100, 700)
            };
            // Optional styling
            linkFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            linkFragment.TextState.FontSize = 14;
            linkFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the TextFragment to the page so the text is rendered
            page.Paragraphs.Add(linkFragment);

            // Create a GoToAction that points to the target page (page 3)
            if (doc.Pages.Count >= 3)
            {
                // Position values are double; cast to float for the Rectangle constructor
                float x = (float)linkFragment.Position.XIndent;
                float y = (float)linkFragment.Position.YIndent;
                float width = 120f;   // enough to contain the text "Go to Page 3"
                float height = 20f;   // approximate height of the text line

                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                // Create the annotation and assign the GoToAction
                LinkAnnotation linkAnnotation = new LinkAnnotation(page, linkRect)
                {
                    Action = new GoToAction(doc.Pages[3])
                };

                // Add the annotation to the page
                page.Annotations.Add(linkAnnotation);
            }
            else
            {
                Console.Error.WriteLine("Target page does not exist.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with text link saved to '{outputPath}'.");
    }
}