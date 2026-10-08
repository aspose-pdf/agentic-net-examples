using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution, PngDevice

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "EvenPagesImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the original PDF
        using (Document originalDoc = new Document(inputPath))
        {
            // -------------------------------------------------------------------
            // 1. Apply a zoom of 0.8 (80 %) to every even‑numbered page.
            //    The PdfPageEditor.Zoom property is the correct way to change the
            //    visual size of pages – PdfViewer does not expose a Zoom API.
            // -------------------------------------------------------------------
            int[] evenPages = Enumerable.Range(1, originalDoc.Pages.Count)
                                         .Where(p => p % 2 == 0)
                                         .ToArray();

            // Save a temporary PDF that contains the zoomed pages.
            // If there are no even pages we simply copy the original document.
            using (MemoryStream editedStream = new MemoryStream())
            {
                if (evenPages.Length > 0)
                {
                    using (PdfPageEditor editor = new PdfPageEditor())
                    {
                        editor.BindPdf(originalDoc);
                        editor.ProcessPages = evenPages;   // target even pages only
                        editor.Zoom = 0.8f;                // 80 % magnification
                        editor.Save(editedStream);
                    }
                }
                else
                {
                    // No even pages – just write the original PDF to the stream.
                    originalDoc.Save(editedStream);
                }

                editedStream.Position = 0; // reset for reading

                // -------------------------------------------------------------------
                // 2. Render each page (now with the applied zoom) to PNG images.
                //    Use the PngDevice (Aspose.Pdf.Devices) – the PdfConverter class
                //    does not expose a SavePageAsImage method.
                // -------------------------------------------------------------------
                using (Document editedDoc = new Document(editedStream))
                {
                    // Define the resolution for the output images (150 DPI in this example).
                    var resolution = new Resolution(150);
                    var pngDevice = new PngDevice(resolution);

                    for (int pageNum = 1; pageNum <= editedDoc.Pages.Count; pageNum++)
                    {
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            // Render the current page to the memory stream.
                            pngDevice.Process(editedDoc.Pages[pageNum], imgStream);
                            imgStream.Position = 0;

                            string imgPath = Path.Combine(outputDir, $"Page_{pageNum}.png");
                            File.WriteAllBytes(imgPath, imgStream.ToArray());
                            Console.WriteLine($"Page {pageNum} saved → {imgPath}");
                        }
                    }
                }
            }
        }
    }
}
