using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerText = "Confidential – Page footer text that may be long and needs word‑by‑word wrapping.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Determine page dimensions (points)
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Height of the footer area (in points)
                double rectHeight = 50;

                // Create a TextFragment for the footer
                var fragment = new TextFragment(footerText);

                // Configure the rectangle that defines the wrapping area
                // TextFragment.Rectangle is read‑only, but the returned Rectangle object is mutable
                fragment.Rectangle.LLX = 0;                                 // left
                fragment.Rectangle.LLY = pageHeight - rectHeight;          // bottom
                fragment.Rectangle.URX = pageWidth;                        // right
                fragment.Rectangle.URY = pageHeight;                       // top

                // Configure text appearance – TextState is also read‑only, modify its members directly
                fragment.TextState.Font = FontRepository.FindFont("Arial");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Color.Gray;

                // Add the fragment to the page – it will wrap word‑by‑word inside the rectangle
                page.Paragraphs.Add(fragment);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer added to all pages. Output saved to '{outputPath}'.");
    }
}
