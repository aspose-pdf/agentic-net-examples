using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // needed for Color, etc.
using PdfRect = Aspose.Pdf.Rectangle; // disambiguate Rectangle

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF
        const string outputSvgPath = "extracted.svg"; // destination SVG
        const int graphicIndex = 1;                    // 1‑based index of the graphic element on the page

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // For this example we look at the first page only
            Page page = pdfDoc.Pages[1]; // 1‑based indexing (global rule)

            // Locate the graphic by its 1‑based index
            int currentIndex = 0;
            XImage? targetImage = null;

            foreach (XImage img in page.Resources.Images)
            {
                currentIndex++;
                if (currentIndex == graphicIndex)
                {
                    targetImage = img;
                    break;
                }
            }

            if (targetImage == null)
            {
                Console.Error.WriteLine($"Graphic element #{graphicIndex} not found on page 1.");
                return;
            }

            // Create a temporary PDF that contains only the extracted image
            Document svgDoc = new Document();
            Page newPage = svgDoc.Pages.Add();

            // Save the XImage into a memory stream and add it to the new page resources
            using (MemoryStream imgStream = new MemoryStream())
            {
                targetImage.Save(imgStream);
                imgStream.Position = 0; // reset stream position
                string imgName = newPage.Resources.Images.Add(imgStream);

                // Define a rectangle that covers the whole page (or adjust as needed)
                PdfRect rect = new PdfRect(0, 0, newPage.PageInfo.Width, newPage.PageInfo.Height);
                newPage.AddImage(imgName, rect);
            }

            // Save the temporary PDF as SVG
            SvgSaveOptions svgOpts = new SvgSaveOptions();
            svgDoc.Save(outputSvgPath, svgOpts);

            Console.WriteLine($"Graphic element #{graphicIndex} saved as SVG to '{outputSvgPath}'.");
        }
    }
}
