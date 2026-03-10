using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToURIAction and explicit destinations

class Program
{
    static void Main()
    {
        // Paths for the input TeX file and the resulting PDF with a link annotation.
        const string texPath = "input.tex";
        const string pdfPath = "output.pdf";

        // Ensure the TeX source exists.
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Load the TeX file and convert it to a PDF document.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document pdfDocument = new Document(texPath, texLoadOptions))
        {
            // Choose the page where the link annotation will be placed.
            // Aspose.Pdf uses 1‑based indexing for pages.
            Page page = pdfDocument.Pages[1];

            // Define the rectangle (in points) that represents the clickable area.
            // Fully qualify the Rectangle type to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation.
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Optional visual appearance: blue border.
                Color = Aspose.Pdf.Color.Blue,
                // Set the action to open an external URL.
                Action = new GoToURIAction("https://www.example.com")
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(link);

            // Save the modified document as PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{pdfPath}'.");
    }
}