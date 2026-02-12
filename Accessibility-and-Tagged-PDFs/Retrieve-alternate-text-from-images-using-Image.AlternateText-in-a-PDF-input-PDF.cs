using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for ImagePlacementAbsorber

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (change as needed)
        const string inputPdfPath = "input.pdf";

        // Verify that the PDF file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                Page page = pdfDocument.Pages[pageNumber];

                // Use ImagePlacementAbsorber to locate images on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each found image
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // XImage object representing the image resource
                    XImage xImage = imgPlacement.Image;

                    // Retrieve alternate text for this image on the current page
                    IList<string> altTexts = xImage.GetAlternativeText(page);

                    // If alternate text exists, display it
                    if (altTexts != null && altTexts.Count > 0)
                    {
                        Console.WriteLine($"Page {pageNumber}, Image '{xImage.Name}':");
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
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}