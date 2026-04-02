using System;
using System.IO;
using Aspose.Pdf;

public class TrimWhiteSpace
{
    public static void Main()
    {
        // Input and output PDF file names (relative to the executable folder)
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Custom tolerance in points (1 point = 1/72 inch). Adjust as needed.
        const float tolerance = 5f;

        // ---------------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a minimal PDF so
        // the sample can run without throwing a FileNotFoundException.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            // Create a one‑page PDF with default size (A4) as a placeholder.
            using (Document placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Load the document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Current TrimBox of the page
                Rectangle currentTrim = page.TrimBox;

                // Guard against over‑trimming that would produce negative width/height
                float newLlx = (float)Math.Min(currentTrim.LLX + tolerance, currentTrim.URX - tolerance);
                float newLly = (float)Math.Min(currentTrim.LLY + tolerance, currentTrim.URY - tolerance);
                float newUrx = (float)Math.Max(currentTrim.URX - tolerance, currentTrim.LLX + tolerance);
                float newUry = (float)Math.Max(currentTrim.URY - tolerance, currentTrim.LLY + tolerance);

                // Create a new TrimBox reduced by the tolerance on each side
                Rectangle newTrim = new Rectangle(newLlx, newLly, newUrx, newUry);

                // Apply the new TrimBox
                page.TrimBox = newTrim;
            }

            // Save the modified document
            doc.Save(outputPath);
        }
    }
}
