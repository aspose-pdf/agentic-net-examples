using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_out.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            Page page = doc.Pages[1];

            // MediaBox – the physical size of the page (largest box)
            Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
            Console.WriteLine($"MediaBox: {BoxToString(mediaBox)}");

            // CropBox – region that is displayed/printed; defaults to MediaBox if not set
            Aspose.Pdf.Rectangle cropBox = page.CropBox;
            Console.WriteLine($"CropBox : {BoxToString(cropBox)}");

            // TrimBox – final size after trimming (used by printers)
            Aspose.Pdf.Rectangle trimBox = page.TrimBox;
            Console.WriteLine($"TrimBox : {BoxToString(trimBox)}");

            // BleedBox – area that may contain content extending beyond TrimBox for bleed
            Aspose.Pdf.Rectangle bleedBox = page.BleedBox;
            Console.WriteLine($"BleedBox: {BoxToString(bleedBox)}");

            // ArtBox – region that contains the meaningful artwork (often same as TrimBox)
            Aspose.Pdf.Rectangle artBox = page.ArtBox;
            Console.WriteLine($"ArtBox  : {BoxToString(artBox)}");

            // Rect – convenience property:
            //   Get: returns CropBox if defined, otherwise MediaBox.
            //   Set: always modifies MediaBox (ignores rotation).
            Aspose.Pdf.Rectangle rect = page.Rect;
            Console.WriteLine($"Rect    : {BoxToString(rect)}");

            // Example modification: reset origin to (0,0) while keeping size
            page.Rect = new Aspose.Pdf.Rectangle(0, 0, mediaBox.Width, mediaBox.Height);

            // Save the document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine("Processing completed.");
    }

    static string BoxToString(Aspose.Pdf.Rectangle r)
    {
        if (r == null) return "null";
        return $"LLX={r.LLX}, LLY={r.LLY}, URX={r.URX}, URY={r.URY}";
    }
}