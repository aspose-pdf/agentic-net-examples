using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document must contain at least 4 pages.");
                return;
            }

            // Get page four (1‑based indexing)
            Page page = doc.Pages[4];

            // Define the button rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on page four
            ButtonField button = new ButtonField(page, rect)
            {
                Name           = "AlertButton",
                AlternateName  = "ShowAlert",
                NormalCaption  = "Click Me"
            };

            // Assign a JavaScript action that shows an alert when the button is pressed
            button.Actions.OnPressMouseBtn = new JavascriptAction("app.alert('Button clicked!');");

            // Add the button to the page's annotation collection
            page.Annotations.Add(button);

            // Save the modified PDF (PDF format, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation added and saved to '{outputPath}'.");
    }
}