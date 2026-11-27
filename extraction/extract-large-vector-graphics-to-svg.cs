using System;
using System.IO;
using Aspose.Pdf;

class ExtractVectorGraphics
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputSvgPath = "page21_vectors.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (global rule)
            const int pageNumber = 21;
            if (pageNumber > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {sourceDoc.Pages.Count} pages.");
                return;
            }

            // Get page 21
            Page sourcePage = sourceDoc.Pages[pageNumber];

            // Create a new PDF that will contain only the filtered vector graphics
            using (Document targetDoc = new Document())
            {
                // Add a blank page to the target document
                Page targetPage = targetDoc.Pages.Add();

                int xObjectCounter = 1;

                // Iterate over Form XObjects (vector graphics) via Resources.Forms
                foreach (Aspose.Pdf.XForm form in sourcePage.Resources.Forms)
                {
                    // Ensure the form has a bounding box
                    if (form.BBox != null)
                    {
                        // Calculate height of the bounding box (URY - LLY)
                        double height = form.BBox.URY - form.BBox.LLY;

                        // Filter: keep only elements with height > 200 points
                        if (height > 200)
                        {
                            // Assign a unique name to the XForm (required for later reference)
                            string xObjectName = $"FXO{xObjectCounter++}";
                            form.Name = xObjectName;

                            // Add the filtered XForm to the target page's resources (Add takes a single argument)
                            targetPage.Resources.Forms.Add(form);
                        }
                    }
                }

                // Save the target document as SVG using explicit SvgSaveOptions
                // (save-to-non-pdf-always-use-save-options rule)
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                targetDoc.Save(outputSvgPath, svgOptions);
            }
        }

        Console.WriteLine($"Vector graphics extracted and saved to '{outputSvgPath}'.");
    }
}
