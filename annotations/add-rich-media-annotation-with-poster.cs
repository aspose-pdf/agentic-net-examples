using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string posterPath = "poster.jpg";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(posterPath))
        {
            Console.Error.WriteLine($"Poster image not found: {posterPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document has at least 3 pages.
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document does not contain page 3.");
                return;
            }

            // Get page 3 (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[3];

            // Define the annotation rectangle (llx, lly, urx, ury) in points.
            // Adjust coordinates as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation on page 3.
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Set the poster image for the annotation. The SetPoster overload expects a Stream.
            using (FileStream posterStream = File.OpenRead(posterPath))
            {
                richMedia.SetPoster(posterStream);
            }

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(richMedia);

            // Update the annotation to apply changes.
            richMedia.Update();

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPath}'.");
    }
}
