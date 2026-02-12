using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input PDF
        const string inputPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
            {
                Page page = pdfDocument.Pages[pageNum];

                // Find all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each image found on the page
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    XImage xImage = placement.Image;

                    // Retrieve alternate text for the image on this page
                    IList<string> altTexts = xImage.GetAlternativeText(page);

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        Console.WriteLine($"Page {pageNum}, Image Name: {xImage.Name}");
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"  Alt Text: {alt}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}