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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Blue,          // Visual appearance
                Contents = "Click to display word count"   // Tooltip text
            };

            // JavaScript that calculates total word count and shows an alert
            string js = @"
var total = 0;
for (var i = 1; i <= this.numPages; i++) {
    total += this.getPageNumWords(i);
}
app.alert('Word count: ' + total);
";

            // Assign the JavaScript action to the annotation
            link.Action = new JavascriptAction(js);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link annotation: {outputPath}");
    }
}