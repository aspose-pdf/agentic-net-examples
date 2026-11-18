using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string placeholderImgPath = "placeholder.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(placeholderImgPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImgPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Find the first page that contains the word "Appendix"
            int appendixStartPage = -1;
            TextAbsorber absorber = new TextAbsorber();
            absorber.TextSearchOptions = new TextSearchOptions(true); // case‑insensitive
            doc.Pages.Accept(absorber);
            string fullText = absorber.Text; // currently unused but kept for possible future logic

            // Simple heuristic: iterate pages until the keyword is found
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                TextAbsorber pageAbsorber = new TextAbsorber();
                pageAbsorber.TextSearchOptions = new TextSearchOptions(true);
                doc.Pages[i].Accept(pageAbsorber);
                if (!string.IsNullOrEmpty(pageAbsorber.Text) &&
                    pageAbsorber.Text.IndexOf("Appendix", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    appendixStartPage = i;
                    break;
                }
            }

            if (appendixStartPage == -1)
            {
                Console.WriteLine("No appendix section detected. No changes applied.");
                doc.Save(outputPdfPath);
                return;
            }

            // Iterate over all pages belonging to the appendix and replace images with a placeholder
            for (int pageNum = appendixStartPage; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Remove all existing images from the page resources
                page.Resources.Images.Clear();

                // Create an ImageStamp that references the placeholder file directly
                ImageStamp placeholderStamp = new ImageStamp(placeholderImgPath);
                // Position the placeholder (example: top‑left corner, 100x100 points)
                placeholderStamp.XIndent = 0;
                placeholderStamp.YIndent = page.PageInfo.Height - 100; // from top
                placeholderStamp.Width = 100;
                placeholderStamp.Height = 100;
                placeholderStamp.Background = false; // draw over content

                // Apply the stamp to the page
                page.AddStamp(placeholderStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }
}
