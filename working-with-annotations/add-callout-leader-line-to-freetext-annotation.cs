using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;      // for DefaultAppearance
using System.Drawing;      // for System.Drawing.Color (required by DefaultAppearance)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_callout.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];   // 1‑based indexing

            // Define the rectangle for the free‑text annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance (font name, size, text color)
            // Note: the constructor requires System.Drawing.Color for the color argument
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation
            FreeTextAnnotation ft = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Callout example",
                Color    = Aspose.Pdf.Color.Yellow,               // border color
                Intent   = FreeTextIntent.FreeTextCallout        // indicate this is a callout
            };

            // Set the Callout property – exactly three points:
            //   1. Start point (inside the annotation)
            //   2. Knee point (bend of the leader line)
            //   3. End point (target point the line points to)
            ft.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 525), // start (inside the annotation)
                new Aspose.Pdf.Point(200, 600), // knee (bend)
                new Aspose.Pdf.Point(250, 650)  // end (pointing to target)
            };

            // Add the annotation to the page
            page.Annotations.Add(ft);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with callout annotation to '{outputPath}'.");
    }
}