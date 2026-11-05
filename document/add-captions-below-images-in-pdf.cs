using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_captions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int pageNumber = 1;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                int imageIndex = 1;

                // Iterate over the images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a simple caption text (e.g., "Figure 2‑3")
                    string caption = $"Figure {pageNumber}-{imageIndex}";

                    // Create a styled text fragment for the caption
                    TextFragment captionFragment = new TextFragment(caption);
                    captionFragment.TextState.FontSize = 12;                                   // readable size
                    captionFragment.TextState.FontStyle = FontStyles.Bold;                     // bold style
                    captionFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;        // gray color
                    captionFragment.TextState.Font = FontRepository.FindFont("Helvetica");    // standard font

                    // Position the caption near the bottom of the page.
                    // Precise placement directly under the image would require parsing the content stream;
                    // here we place it at a fixed offset from the bottom margin.
                    captionFragment.Position = new Position(0, 20); // 20 points above the page bottom

                    // Add the caption to the page's paragraph collection
                    page.Paragraphs.Add(captionFragment);

                    imageIndex++;
                }

                pageNumber++;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Captions added. Output saved to '{outputPath}'.");
    }
}