using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output.pdf";         // result PDF
        const string logoImage  = "logo.png";           // corporate logo to use as watermark

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            // Collect pages that contain WatermarkAnnotations and remember their opacity
            var pagesToStamp = new List<int>();
            var pageOpacity  = new Dictionary<int, float>();

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageNum];

                // Scan annotations backwards so we can delete them safely
                for (int annIdx = page.Annotations.Count; annIdx >= 1; annIdx--)
                {
                    Aspose.Pdf.Annotations.Annotation ann = page.Annotations[annIdx];
                    if (ann is Aspose.Pdf.Annotations.WatermarkAnnotation wmAnn)
                    {
                        // Preserve the opacity of the original watermark (cast double -> float)
                        float opacity = (float)wmAnn.Opacity;
                        pagesToStamp.Add(pageNum);
                        // If the page already has an entry, keep the first encountered opacity
                        if (!pageOpacity.ContainsKey(pageNum))
                            pageOpacity[pageNum] = opacity;

                        // Remove the existing WatermarkAnnotation
                        page.Annotations.Delete(annIdx);
                    }
                }
            }

            // If no WatermarkAnnotations were found, just save the original document
            if (pagesToStamp.Count == 0)
            {
                doc.Save(outputPdf);
                Console.WriteLine("No WatermarkAnnotations found – document saved unchanged.");
                return;
            }

            // Use PdfFileStamp (Facade) to add the new image watermark
            using (Aspose.Pdf.Facades.PdfFileStamp stampFacade = new Aspose.Pdf.Facades.PdfFileStamp())
            {
                // Bind the already loaded document
                stampFacade.BindPdf(doc);

                // Create a stamp that uses the corporate logo image
                Aspose.Pdf.Facades.Stamp imageStamp = new Aspose.Pdf.Facades.Stamp();
                imageStamp.BindImage(logoImage);
                imageStamp.IsBackground = true;               // place behind page content

                // Apply the stamp to each page, preserving its original opacity
                foreach (int pg in pagesToStamp)
                {
                    // Set opacity for this specific page
                    imageStamp.Opacity = pageOpacity[pg];

                    // Restrict the stamp to the current page only
                    imageStamp.Pages = new int[] { pg };

                    // Add the stamp to the document
                    stampFacade.AddStamp(imageStamp);
                }

                // Save the modified PDF
                stampFacade.Save(outputPdf);
                stampFacade.Close();
            }

            Console.WriteLine($"Watermarks replaced and saved to '{outputPdf}'.");
        }
    }
}
