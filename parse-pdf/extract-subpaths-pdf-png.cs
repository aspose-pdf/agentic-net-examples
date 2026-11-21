using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "GraphicsImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing rule)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                int subPathCounter = 1;
                bool anyGraphicsFound = false;

                // Use GraphicsAbsorber to collect vector graphic elements on the page
                using (var absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        if (element is SubPath subPath)
                        {
                            anyGraphicsFound = true;

                            // Create a temporary PDF that contains only this sub‑path
                            using (Document tempDoc = new Document())
                            {
                                Page tempPage = tempDoc.Pages.Add();

                                // Copy the operators that make up the sub‑path onto the temporary page
                                foreach (Operator op in subPath.Operators)
                                {
                                    tempPage.Contents.Add(op);
                                }

                                string outputPngPath = Path.Combine(
                                    outputFolder,
                                    $"Page{pageIndex}_SubPath{subPathCounter}.png");

                                // Rasterize the temporary page to PNG
                                new PngDevice().Process(tempPage, outputPngPath);
                            }

                            subPathCounter++;
                        }
                    }
                }

                // If no graphics were found on the page, inform the user
                if (!anyGraphicsFound)
                {
                    Console.WriteLine($"No vector graphics found on page {pageIndex}.");
                }
                else
                {
                    Console.WriteLine($"Extracted {subPathCounter - 1} subpaths from page {pageIndex}.");
                }
            }
        }

        Console.WriteLine("Graphics extraction completed.");
    }
}
