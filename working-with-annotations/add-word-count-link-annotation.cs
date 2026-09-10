using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_wordcount_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule‑based lifecycle)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (coordinates are in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Blue;
            // Border must be created after the annotation instance exists
            link.Border = new Border(link)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // JavaScript that calculates total word count and shows an alert
            string js = @"
var total = 0;
for (var i = 0; i < this.numPages; i++) {
    total += this.getPageNumWords(i + 1);
}
app.alert('Word count: ' + total);
";

            // Attach the JavaScript action to the link annotation
            link.Action = new JavascriptAction(js);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF (using rule‑based disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with word‑count link: '{outputPath}'");
    }
}
