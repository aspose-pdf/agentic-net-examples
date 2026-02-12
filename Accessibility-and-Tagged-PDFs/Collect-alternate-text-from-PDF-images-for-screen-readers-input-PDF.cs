using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Search for image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each found image
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // The XImage object representing the image
                    XImage xImage = placement.Image;

                    // Retrieve alternate text for this image on the current page
                    List<string> altTexts = xImage.GetAlternativeText(page);

                    Console.WriteLine($"Page {pageIndex}, Image '{xImage.Name ?? "Unnamed"}':");

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"  Alt: {alt}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No alternate text available.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}