using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // ----- Retrieve current page size -----
                // MediaBox defines the full page rectangle
                double pageWidth = page.MediaBox.URX - page.MediaBox.LLX;
                double pageHeight = page.MediaBox.URY - page.MediaBox.LLY;
                Console.WriteLine($"Page {i}: width={pageWidth}, height={pageHeight}");

                // ----- Retrieve current orientation -----
                // Rotation enum values: on90, on180, on270, None
                Rotation currentRotation = page.Rotate;
                Console.WriteLine($"Page {i}: rotation={currentRotation}");

                // ----- Retrieve current margins (CropBox) -----
                // If CropBox is null, it defaults to MediaBox
                Rectangle crop = page.CropBox ?? page.MediaBox;
                double leftMargin   = crop.LLX - page.MediaBox.LLX;
                double bottomMargin = crop.LLY - page.MediaBox.LLY;
                double rightMargin  = page.MediaBox.URX - crop.URX;
                double topMargin    = page.MediaBox.URY - crop.URY;
                Console.WriteLine($"Page {i}: margins L={leftMargin}, B={bottomMargin}, R={rightMargin}, T={topMargin}");

                // ----- Modify page size to A4 (595 x 842 points) -----
                page.Resize(PageSize.A4);

                // ----- Ensure portrait orientation -----
                page.Rotate = Rotation.None; // no rotation

                // ----- Set uniform 36‑point margins (0.5 inch) -----
                double margin = 36; // points
                page.CropBox = new Rectangle(
                    page.MediaBox.LLX + margin,
                    page.MediaBox.LLY + margin,
                    page.MediaBox.URX - margin,
                    page.MediaBox.URY - margin);

                // Align other optional boxes with the new CropBox
                page.BleedBox = page.CropBox;
                page.TrimBox  = page.CropBox;
                page.ArtBox   = page.CropBox;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
