using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";
        const float zoomFactor  = 0.5f; // 50% zoom

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using and Document.Save)
        using (Document doc = new Document(inputPath))
        {
            // Collect page numbers that contain at least one image
            List<int> pagesWithImages = new List<int>();

            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // ImagePlacementAbsorber finds image placements on a page
                ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                doc.Pages[pageNum].Accept(imgAbsorber);

                if (imgAbsorber.ImagePlacements.Count > 0)
                {
                    pagesWithImages.Add(pageNum);
                }
            }

            if (pagesWithImages.Count > 0)
            {
                // Use PdfPageEditor to apply zoom only to the collected pages
                PdfPageEditor editor = new PdfPageEditor();

                // Bind the already loaded document to the editor
                editor.BindPdf(doc);

                // Specify which pages to edit
                editor.ProcessPages = pagesWithImages.ToArray();

                // Set the desired zoom factor (1.0 = 100%)
                editor.Zoom = zoomFactor;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}