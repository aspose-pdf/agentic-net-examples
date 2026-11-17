using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create the link annotation – note the required (page, rectangle) constructor
            LinkAnnotation link = new LinkAnnotation(page, linkRect);

            // JavaScript code: iterate all pages, sum word counts, and show an alert
            string jsCode = @"
var total = 0;
for (var i = 0; i < this.numPages; i++) {
    total += this.getPageNumWords(i);
}
app.alert('Document word count: ' + total);
";

            // Attach the JavaScript action to the link annotation
            link.Action = new JavascriptAction(jsCode);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript link saved to '{outputPath}'.");
    }
}
