using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("Document must contain at least 10 pages.");
                return;
            }

            // Choose the page where the button will be placed (e.g., first page)
            Page targetPage = doc.Pages[1]; // 1‑based indexing (rule: page-indexing-one-based)

            // Define the button rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);

            // Create the button field on the chosen page
            ButtonField button = new ButtonField(targetPage, btnRect);
            // Set visual and caption properties
            button.Color = Aspose.Pdf.Color.LightGray;
            button.NormalCaption = "Go to Page 10";
            button.AlternateCaption = "Going...";
            // Assign a border after the button instance is fully created (rule: border-requires-parent-annotation)
            button.Border = new Border(button) { Width = 1 };

            // Create a GoToAction that points to page 10 of the same document
            Page pageTen = doc.Pages[10]; // 1‑based indexing
            GoToAction goToPageTen = new GoToAction(pageTen);

            // Assign the action to the button's activation event
            button.OnActivated = goToPageTen;

            // Add the button to the page's annotation collection
            targetPage.Annotations.Add(button);

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation added. Saved to '{outputPath}'.");
    }
}
