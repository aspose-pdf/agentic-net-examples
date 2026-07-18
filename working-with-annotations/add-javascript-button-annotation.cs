using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 4 pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document does not contain a fourth page.");
                return;
            }

            // Get page 4 (1‑based indexing)
            Page page = doc.Pages[4];

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the button field on page 4
            ButtonField button = new ButtonField(page, rect)
            {
                Name = "AlertButton",
                NormalCaption = "Show Alert"
            };

            // Assign JavaScript to be executed on mouse press
            button.Actions.OnPressMouseBtn = new JavascriptAction("app.alert('Button clicked!');");

            // Add the button to the page annotations collection
            page.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}