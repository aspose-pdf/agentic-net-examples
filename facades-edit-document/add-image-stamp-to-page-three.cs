using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logoPath = "logo.png";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Initialise the facade – use BindPdf and Save (old InputFile/OutputFile are obsolete)
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a stamp and bind the logo image (use fully‑qualified Stamp to avoid ambiguity)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(logoPath);

        // Define the visual size of the image stamp (width, height in points)
        const float stampWidth = 100f;
        const float stampHeight = 50f;
        stamp.SetImageSize(stampWidth, stampHeight);

        // Determine page‑3 dimensions to calculate bottom‑right coordinates
        using (Document srcDoc = new Document(inputPath))
        {
            if (srcDoc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                fileStamp.Close();
                return;
            }

            Page pageThree = srcDoc.Pages[3];
            double pageWidth = pageThree.PageInfo.Width;
            // double pageHeight = pageThree.PageInfo.Height; // not needed for bottom‑right placement

            // Margin from the edges (in points)
            const double margin = 10.0;

            // X coordinate: page width minus stamp width minus right margin
            float x = (float)(pageWidth - stampWidth - margin);
            // Y coordinate: bottom margin (origin is lower‑left)
            float y = (float)margin;

            // Position the stamp
            stamp.SetOrigin(x, y);
        }

        // Apply the stamp only to page 3
        stamp.Pages = new int[] { 3 };

        // Add the stamp to the file and finalize
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Logo stamp added to page 3 and saved as '{outputPath}'.");
    }
}
