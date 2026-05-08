using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for ImagePlacementAbsorber

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect page numbers that contain at least one image
            List<int> pagesWithImages = new List<int>();

            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // ImagePlacementAbsorber finds image placements on a page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                doc.Pages[pageNum].Accept(absorber);

                if (absorber.ImagePlacements.Count > 0)
                {
                    pagesWithImages.Add(pageNum);
                }
            }

            // If any pages contain images, apply zoom only to those pages
            if (pagesWithImages.Count > 0)
            {
                // PdfPageEditor works on the same Document instance
                PdfPageEditor editor = new PdfPageEditor();
                editor.BindPdf(doc);

                // Specify the pages to be edited
                editor.ProcessPages = pagesWithImages.ToArray();

                // Set desired zoom factor (e.g., 150% zoom)
                editor.Zoom = 1.5f;

                // Apply the changes to the selected pages
                editor.ApplyChanges();
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}