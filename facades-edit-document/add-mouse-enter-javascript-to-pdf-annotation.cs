using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation – it can host JavaScript actions
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Make the annotation invisible (no visible border or color)
            link.Color = Aspose.Pdf.Color.Transparent; // invisible color
            link.Border = new Border(link) { Width = 0 };

            // JavaScript code that changes the page background color.
            // Adjust the script as needed; this example sets a light gray background.
            string jsCode = "this.bgColor = color.gray;";

            // Assign the JavaScript to the annotation's default action.
            // The LinkAnnotation class does not expose an OnEnter property on its Actions collection
            // in the current Aspose.Pdf version; instead, set the Action property directly.
            link.Action = new JavascriptAction(jsCode);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action to '{outputPath}'.");
    }
}
