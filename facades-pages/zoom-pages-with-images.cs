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

        // Collect page numbers that contain at least one image
        List<int> pagesWithImages = new List<int>();

        using (Document doc = new Document(inputPath))
        {
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Absorb image placements on the current page
                ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                doc.Pages[pageNum].Accept(imgAbsorber);

                // If any image is found, remember this page number
                if (imgAbsorber.ImagePlacements.Count > 0)
                {
                    pagesWithImages.Add(pageNum);
                }
            }
        }

        // If no pages contain images, simply copy the source file
        if (pagesWithImages.Count == 0)
        {
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("No images found – source PDF copied unchanged.");
            return;
        }

        // Apply zoom only to the identified pages using PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the original PDF file
            editor.BindPdf(inputPath);

            // Restrict editing to pages that have images
            editor.ProcessPages = pagesWithImages.ToArray();

            // Set the desired zoom factor (1.0 = 100%)
            editor.Zoom = zoomFactor;

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied to pages with images. Output saved to '{outputPath}'.");
    }
}