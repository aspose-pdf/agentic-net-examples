using System;
using System.IO;
using Aspose.Pdf;

class TrimWhiteSpace
{
    // Convert pixel tolerance to PDF points (1 pixel = 0.75 points at 96 DPI)
    static double PixelsToPoints(double pixels) => pixels * 0.75;

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Custom tolerance per page (in pixels). Adjust as needed.
        double[] pixelTolerances = { 10, 15, 20 }; // example values for first three pages

        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];

                // Determine tolerance for this page (fallback to last value if fewer entries)
                double tolerancePixels = pixelTolerances.Length >= i
                    ? pixelTolerances[i - 1]
                    : pixelTolerances[pixelTolerances.Length - 1];

                double tolerancePoints = PixelsToPoints(tolerancePixels);

                // If the page is considered blank, skip trimming
                if (page.IsBlank(0.01)) // very low fill threshold to treat almost empty pages as blank
                    continue;

                // Get current TrimBox (if not set, use MediaBox as a fallback)
                Aspose.Pdf.Rectangle currentTrim = page.TrimBox ?? page.MediaBox;

                // Create a new rectangle inset by the tolerance on all sides
                double llx = currentTrim.LLX + tolerancePoints;
                double lly = currentTrim.LLY + tolerancePoints;
                double urx = currentTrim.URX - tolerancePoints;
                double ury = currentTrim.URY - tolerancePoints;

                // Ensure the rectangle remains valid
                if (llx < urx && lly < ury)
                {
                    page.TrimBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}