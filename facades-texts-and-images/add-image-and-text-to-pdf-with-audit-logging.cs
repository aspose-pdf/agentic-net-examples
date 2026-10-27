using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.jpg";
        const string textToAdd = "Sample text added.";
        const string auditLog  = "audit.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create or overwrite the audit log
        using (var logWriter = new StreamWriter(auditLog, false))
        {
            // Load the source PDF using the high‑level Document API
            var pdfDocument = new Document(inputPdf);

            // Page numbers are 1‑based
            int pageNumber = 1;

            // ------------------------------------------------------------
            // Insert an image (coordinates: lower‑left x,y and upper‑right x,y)
            // ------------------------------------------------------------
            double llx = 100, lly = 500, urx = 300, ury = 700;
            var imageStamp = new ImageStamp(imagePath)
            {
                // Position – lower‑left corner
                XIndent = llx,
                YIndent = lly,
                // Size – width/height derived from the rectangle
                Width  = urx - llx,
                Height = ury - lly
            };
            pdfDocument.Pages[pageNumber].AddStamp(imageStamp);
            logWriter.WriteLine($"{DateTime.UtcNow:u} | Page {pageNumber} | AddImage | {Path.GetFileName(imagePath)}");

            // ------------------------------------------------------------
            // Insert text (coordinates: lower‑left x,y)
            // ------------------------------------------------------------
            double txtX = 100, txtY = 450;
            var textFragment = new TextFragment(textToAdd)
            {
                Position = new Position(txtX, txtY)
            };
            pdfDocument.Pages[pageNumber].Paragraphs.Add(textFragment);
            logWriter.WriteLine($"{DateTime.UtcNow:u} | Page {pageNumber} | AddText | \"{textToAdd}\"");

            // Save the modified PDF
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'. Audit log written to '{auditLog}'.");
    }
}
