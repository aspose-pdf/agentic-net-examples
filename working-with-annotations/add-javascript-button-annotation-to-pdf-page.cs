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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least four pages (1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Get page four
            Page page = doc.Pages[4];

            // Define the button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a button field on the specified page and rectangle
            ButtonField button = new ButtonField(page, rect);
            // Set the internal name of the field
            button.PartialName = "AlertButton";
            // Set the visible caption of the button
            button.AlternateCaption = "Show Alert";
            // Assign a JavaScript action that displays an alert when the button is clicked
            button.Actions.OnPressMouseBtn = new JavascriptAction("app.alert('Button clicked!');");

            // Add the button to the document's form (this also registers the annotation on the page)
            doc.Form.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation added and saved to '{outputPath}'.");
    }
}
