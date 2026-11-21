using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Define the annotation rectangle (lower‑left X,Y and upper‑right X,Y)
        var rect = new Rectangle(100, 500, 300, 550);

        // Title of the popup annotation
        string title = "Detailed Comments";

        // Multi‑line content with bullet points (use Unicode bullet \u2022)
        string contents = "\u2022 First point\r\n" +
                          "\u2022 Second point with more detail\r\n" +
                          "\u2022 Third point";

        // Create a TextAnnotation (acts as a popup) on page 1
        Page page = doc.Pages[1]; // pages are 1‑based
        var textAnnotation = new TextAnnotation(page, rect)
        {
            Title = title,
            Contents = contents,
            Open = true,               // show the popup automatically
            Icon = TextIcon.Note       // standard note icon
        };

        // Add the annotation to the page
        page.Annotations.Add(textAnnotation);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}
