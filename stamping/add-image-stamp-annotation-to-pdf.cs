using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the stamp will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a StampAnnotation on the target page
            StampAnnotation stamp = new StampAnnotation(page, stampRect);

            // Load the image bytes and assign a stream to the annotation
            byte[] imgBytes = File.ReadAllBytes(stampImagePath);
            stamp.Image = new MemoryStream(imgBytes);

            // Optional visual settings
            stamp.Color = Aspose.Pdf.Color.Transparent; // No border color
            stamp.Opacity = 0.5;                         // Semi‑transparent

            // Add the stamp annotation to the page's annotation collection
            page.Annotations.Add(stamp);

            // Save the modified PDF (annotations are preserved)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
    }
}
