using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Select the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the clickable area for the link annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Visual appearance of the link (optional)
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Show word count"
            };

            // JavaScript that calculates the total word count of the document
            // and displays it in an alert dialog
            string js = "app.alert('Word count: ' + this.numWords);";

            // Assign a JavascriptAction to the annotation (correct API usage)
            link.Action = new JavascriptAction(js);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF (lifecycle rule: use Save within using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with word‑count link saved to '{outputPath}'.");
    }
}