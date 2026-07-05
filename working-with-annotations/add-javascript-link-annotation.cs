using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "LinkWithJs.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the clickable area for the link annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Set a visible border (optional) – border color is set via the annotation's Color property
            link.Color = Aspose.Pdf.Color.Blue; // border color
            link.Border = new Border(link) { Width = 1 };

            // Set the JavaScript action to display a modal dialog
            link.Action = new JavascriptAction("app.alert('This is a custom modal dialog box');");

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript link annotation saved to '{outputPath}'.");
    }
}
