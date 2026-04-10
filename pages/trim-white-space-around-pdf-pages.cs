using System;
using System.IO;
using Aspose.Pdf;

class TrimWhiteSpace
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Calculate the bounding box of the actual content.
                // This method analyses the page content (vector graphics, text, images)
                // and returns the smallest rectangle that encloses everything.
                Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

                // If the content box is valid, assign it to the TrimBox.
                // TrimBox defines the visible area after trimming white space.
                if (contentBox != null && contentBox.Width > 0 && contentBox.Height > 0)
                {
                    page.TrimBox = contentBox;
                }
                else
                {
                    // Fallback: if the page appears blank, keep the original media box.
                    // The IsBlank method uses a default fill‑threshold factor (e.g., 0.01).
                    // Here we use the default threshold of 0.01 for safety.
                    bool isBlank = page.IsBlank(0.01);
                    if (isBlank)
                    {
                        // For a completely blank page we can set TrimBox to MediaBox
                        // to avoid accidental cropping.
                        page.TrimBox = page.MediaBox;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}