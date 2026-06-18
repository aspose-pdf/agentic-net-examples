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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the link annotation
            Page page = doc.Pages[1];

            // Define the clickable area (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Blue,   // visual cue
                Contents = "Show word count"        // tooltip text
            };

            // JavaScript that counts words in the whole document and shows an alert
            string js = @"
var total = 0;
for (var i = 1; i <= this.numPages; i++) {
    total += this.getPageNumWords(i);
}
app.alert('Word count: ' + total);
";

            // Assign the JavaScript action to the link
            link.Action = new JavascriptAction(js);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with word‑count link saved to '{outputPath}'.");
    }
}