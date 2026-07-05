using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 4 exists (pages are 1‑based)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            // Define the button rectangle (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on page 4
            ButtonField button = new ButtonField(doc.Pages[4], buttonRect)
            {
                Name = "AlertButton",
                NormalCaption = "Click Me"
            };

            // Assign a JavaScript action that shows an alert when the button is pressed
            button.Actions.OnPressMouseBtn = new JavascriptAction("app.alert('Button clicked!');");

            // Add the button to the page's annotation collection
            doc.Pages[4].Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with button saved to '{outputPath}'.");
    }
}