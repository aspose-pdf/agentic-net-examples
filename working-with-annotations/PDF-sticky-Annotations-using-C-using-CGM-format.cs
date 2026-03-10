using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file and convert it to a PDF document.
        CgmLoadOptions loadOptions = new CgmLoadOptions(); // default A4 page size
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Create a sticky note (TextAnnotation) on the first page.
            // Rectangle coordinates: lower‑left X/Y, upper‑right X/Y.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            TextAnnotation sticky = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "This is a sticky annotation added after loading CGM.",
                Icon     = TextIcon.Comment,               // choose a visual icon
                Color    = Aspose.Pdf.Color.Yellow,        // annotation color
                Open     = true                            // show popup initially
            };

            // Add the annotation to the page's annotation collection.
            doc.Pages[1].Annotations.Add(sticky);

            // Save the resulting PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sticky annotation saved to '{outputPdf}'.");
    }
}