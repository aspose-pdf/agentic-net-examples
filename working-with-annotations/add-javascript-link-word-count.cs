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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            // Optional visual styling – set after the object is instantiated
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // JavaScript code that calculates the total word count of the document
            // and displays it in an alert dialog.
            string js = @"
var total = 0;
for (var i = 0; i < this.numPages; i++) {
    total += this.getPageNumWords(i + 1);
}
app.alert('Total word count in the document: ' + total);
";

            // Assign a JavascriptAction to the link's Action property
            link.Action = new JavascriptAction(js);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF (lifecycle rule: save within using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with word‑count link: '{outputPath}'");
    }
}
