using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";
        const string linkUrl = "https://www.example.com";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file and convert it to a PDF document.
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Create a link annotation on the first page.
            // Define the annotation rectangle (left, bottom, right, top).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Instantiate the annotation first, then set its properties.
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], rect);
            link.Action = new GoToURIAction(linkUrl);
            link.Color = Aspose.Pdf.Color.Blue;
            // The Border constructor requires the owning annotation, so it must be set after the annotation is created.
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page.
            doc.Pages[1].Annotations.Add(link);

            // Save the resulting PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{pdfPath}'.");
    }
}
