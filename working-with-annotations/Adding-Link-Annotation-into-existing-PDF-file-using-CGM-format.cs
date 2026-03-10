using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputCgmPath  = "input.cgm";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"File not found: {inputCgmPath}");
            return;
        }

        // Load CGM (input‑only format) and convert it to a PDF document
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(inputCgmPath, loadOptions))
        {
            // Add a link annotation on the first page
            Page page = doc.Pages[1]; // 1‑based indexing
            // Define the clickable rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue,
                // Set the action to open an external URL
                Action = new GoToURIAction("https://www.example.com")
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPdfPath}'.");
    }
}