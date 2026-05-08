using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_wordcount_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the link annotation
            Page page = doc.Pages[1];

            // Define the clickable rectangle (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect);
            link.Color = Aspose.Pdf.Color.Blue;                     // visual cue
            link.Border = new Border(link) { Width = 1 };           // thin border

            // JavaScript that calculates and shows the total word count
            string js = "app.alert('Word count: ' + this.numWords);";

            // Assign the JavaScript action to the annotation
            link.Action = new JavascriptAction(js);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with word‑count link: '{outputPath}'.");
    }
}