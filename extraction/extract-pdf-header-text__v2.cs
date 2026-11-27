using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Define a rectangle that covers the header area on the first page.
            // Coordinates are in points (1/72 inch) with origin at lower‑left corner.
            // Adjust llx, lly, urx, ury as needed for your document.
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(
                llx: 0,                                   // left
                lly: doc.Pages[1].PageInfo.Height - 100, // bottom (100 pts from top)
                urx: doc.Pages[1].PageInfo.Width,        // right
                ury: doc.Pages[1].PageInfo.Height        // top
            );

            // Configure TextAbsorber to limit extraction to the defined rectangle.
            // TextAbsorber does not expose LimitToPageBounds or Rectangle directly.
            // Use TextSearchOptions with the rectangle to restrict the search area.
            TextAbsorber absorber = new TextAbsorber();
            absorber.TextSearchOptions = new TextSearchOptions(headerRect);

            // Apply the absorber to the first page (1‑based indexing).
            doc.Pages[1].Accept(absorber);

            // Write the extracted header text to the output file.
            File.WriteAllText(outputTxtPath, absorber.Text ?? string.Empty);
        }

        Console.WriteLine($"Header text extracted to '{outputTxtPath}'.");
    }
}
