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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Define a rectangle that covers the header area on the first page
            // Coordinates are in points (1/72 inch). Adjust as needed.
            // llx, lly = lower‑left corner; urx, ury = upper‑right corner.
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(
                llx: 0,      // left margin
                lly: 750,    // bottom of header (near top of page)
                urx: 595,    // page width for A4 (approx.)
                ury: 842     // top of page for A4 (approx.)
            );

            // Create a TextFragmentAbsorber to extract text within the rectangle
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            // Limit extraction to the defined rectangle using TextSearchOptions
            absorber.TextSearchOptions = new TextSearchOptions(headerRect);

            // Apply the absorber to the first page (1‑based indexing)
            doc.Pages[1].Accept(absorber);

            // Retrieve the extracted header text
            string headerText = absorber.Text ?? string.Empty;

            // Write the extracted text to a plain‑text file
            File.WriteAllText(outputTxtPath, headerText);
        }

        Console.WriteLine($"Header text extracted to '{outputTxtPath}'.");
    }
}
