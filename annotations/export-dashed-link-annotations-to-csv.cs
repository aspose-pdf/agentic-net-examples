using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // not needed but safe for any text handling

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "link_annotations.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("Page,LLX,LLY,URX,URY");

                    // Pages are 1‑based in Aspose.Pdf
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Iterate over all annotations on the page
                        foreach (Annotation annot in page.Annotations)
                        {
                            // We are interested only in LinkAnnotation objects
                            if (annot is LinkAnnotation linkAnnot)
                            {
                                // Check that the border style is dashed
                                // Border may be null, so guard against it
                                if (linkAnnot.Border != null &&
                                    linkAnnot.Border.Style == BorderStyle.Dashed)
                                {
                                    // Use fully qualified Rectangle to avoid ambiguity
                                    Aspose.Pdf.Rectangle rect = linkAnnot.Rect;

                                    // Export rectangle coordinates to CSV
                                    // CSV format: Page,LLX,LLY,URX,URY
                                    writer.WriteLine($"{pageIndex},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}");
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"Export completed. CSV saved to '{csvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}