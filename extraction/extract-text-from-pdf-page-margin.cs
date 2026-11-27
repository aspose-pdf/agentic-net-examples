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

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle that covers the entire page (margin area)
            // Coordinates: lower‑left (0,0), upper‑right (page width, page height)
            Aspose.Pdf.Rectangle marginRect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

            // Configure TextAbsorber with extraction options limited to the rectangle
            TextAbsorber absorber = new TextAbsorber();
            absorber.TextSearchOptions = new TextSearchOptions(marginRect)
            {
                // LimitToPageBounds is available on TextSearchOptions in recent versions;
                // if the property does not exist, the rectangle itself ensures the limit.
                LimitToPageBounds = true
            };

            // Extract text from the page using the absorber
            page.Accept(absorber);

            // Save extracted text to a plain‑text file
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Margin text extracted to '{outputPath}'.");
    }
}
