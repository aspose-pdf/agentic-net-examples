using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "LaunchActionExample.pdf";
        const string externalFilePath = @"C:\Temp\example.txt"; // file to open on click

        // Ensure the external file exists for demonstration purposes
        if (!File.Exists(externalFilePath))
        {
            File.WriteAllText(externalFilePath, "This is the external file opened by the PDF link.");
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Add visible text that the user will click
            TextFragment tf = new TextFragment("Click here to open the external file");
            tf.Position = new Position(100, 700); // place the text near the top of the page
            tf.TextState.FontSize = 14;
            page.Paragraphs.Add(tf);

            // Define a rectangle that covers the text (left, bottom, right, top)
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 680, 350, 720);

            // Create a link annotation with a Launch action
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            // LaunchAction opens an external file when the annotation is activated
            link.Action = new LaunchAction(externalFilePath);

            // Optionally set the appearance of the link (e.g., underline)
            link.Border = new Border(link);
            link.Border.Width = 0; // no visible border
            link.Color = Color.Transparent; // make the annotation invisible

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with launch action saved to '{outputPath}'.");
    }
}