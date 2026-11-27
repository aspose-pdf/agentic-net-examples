using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Use the first page to define the rectangle (middle section)
                Page page = doc.Pages[1];

                double llx = page.PageInfo.Width / 4;          // lower‑left X
                double lly = page.PageInfo.Height / 4;         // lower‑left Y
                double urx = page.PageInfo.Width * 3 / 4;      // upper‑right X
                double ury = page.PageInfo.Height * 3 / 4;     // upper‑right Y

                // Coordinate‑box rectangle (Aspose.Pdf.Rectangle)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Set up the TextFragmentAbsorber with region limits
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.TextSearchOptions.Rectangle = rect;
                absorber.TextSearchOptions.LimitToPageBounds = true;

                // Extract text from the defined region of the page
                page.Accept(absorber);

                string extractedText = absorber.Text ?? string.Empty;

                // Save extracted text to a file
                File.WriteAllText(outputPath, extractedText);
            }

            Console.WriteLine($"Extracted text saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
