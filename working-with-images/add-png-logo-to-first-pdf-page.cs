using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string logoPng   = "logo.png";    // PNG logo to add
        const string outputPdf = "output.pdf";  // result PDF

        // Define logo position: lower‑left (llx,lly) and upper‑right (urx,ury) coordinates
        // Adjust these values as needed for your layout
        const double llx = 50;   // left X
        const double lly = 750;  // bottom Y
        const double urx = 150;  // right X
        const double ury = 800;  // top Y

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPng))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPng}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Open the PNG file as a stream
            using (FileStream imgStream = File.OpenRead(logoPng))
            {
                // Fully qualified rectangle to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Add the image to the page at the specified rectangle
                page.AddImage(imgStream, rect);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added and saved to '{outputPdf}'.");
    }
}