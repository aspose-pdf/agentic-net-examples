using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputSvgPath = "extracted_curves.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 27 pages (Aspose.Pdf uses 1‑based indexing)
            if (srcDoc.Pages.Count < 27)
            {
                Console.Error.WriteLine("The source PDF does not contain page 27.");
                return;
            }

            // Get page 27
            Page srcPage = srcDoc.Pages[27];

            // Create a new PDF that will contain only the CurveTo operators
            using (Document curveDoc = new Document())
            {
                // Add a blank page to the new document (same size as source page for safety)
                Page curvePage = curveDoc.Pages.Add();
                curvePage.PageInfo.Width = srcPage.PageInfo.Width;
                curvePage.PageInfo.Height = srcPage.PageInfo.Height;

                // Iterate over all operators (contents) on the source page
                for (int i = 1; i <= srcPage.Contents.Count; i++)
                {
                    Operator op = srcPage.Contents[i];
                    // Keep only CurveTo operators
                    if (op is CurveTo)
                    {
                        // Add the CurveTo operator to the new page
                        curvePage.Contents.Add(op);
                    }
                }

                // Save the resulting document as SVG
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                curveDoc.Save(outputSvgPath, svgOptions);
            }
        }

        Console.WriteLine($"CurveTo vector graphics extracted to SVG: {outputSvgPath}");
    }
}
