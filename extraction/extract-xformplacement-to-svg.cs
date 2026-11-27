using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputSvg = "element.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the core Document API
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least four pages (pages are 1‑based)
                if (doc.Pages.Count < 4)
                {
                    Console.Error.WriteLine("The document contains fewer than 4 pages.");
                    return;
                }

                // Retrieve page 4
                Page page = doc.Pages[4];

                // Extract vector graphic elements from the page
                using (var absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);

                    // Find the first XFormPlacement element
                    XFormPlacement xForm = null;
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        if (element is XFormPlacement placement)
                        {
                            xForm = placement;
                            break;
                        }
                    }

                    if (xForm == null)
                    {
                        Console.Error.WriteLine("No XFormPlacement found on page 4.");
                        return;
                    }

                    // Save the XFormPlacement as SVG – this operation uses GDI and works only on Windows
                    if (OperatingSystem.IsWindows())
                    {
                        xForm.SaveToSvg(outputSvg);
                        Console.WriteLine($"SVG representation saved to '{outputSvg}'.");
                    }
                    else
                    {
                        Console.Error.WriteLine("SVG export requires Windows OS.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
