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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Get dimensions of the first page (pages are 1‑based).
            Page firstPage = pdfDoc.Pages[1];
            double pageWidth = firstPage.PageInfo.Width;
            double pageHeight = firstPage.PageInfo.Height;

            // Define a rectangle covering the footer area (e.g., bottom 50 points of each page).
            // Rectangle constructor: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, 50);

            // TextSearchOptions now requires a constructor argument (e.g., search in annotations).
            TextSearchOptions searchOptions = new TextSearchOptions(true)
            {
                Rectangle = footerRect,
                LimitToPageBounds = true
            };

            // Configure TextAbsorber to search only within the footer rectangle and stay within page bounds.
            TextAbsorber absorber = new TextAbsorber
            {
                TextSearchOptions = searchOptions
            };

            // Extract text from all pages respecting the rectangle settings.
            pdfDoc.Pages.Accept(absorber);

            // Write the extracted footer text to a plain text file.
            File.WriteAllText(outputTxtPath, absorber.Text);
        }

        Console.WriteLine($"Footer text extracted to '{outputTxtPath}'.");
    }
}
