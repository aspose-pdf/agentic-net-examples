using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution struct

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ZoomedPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document once – it will be reused for each page.
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            for (int i = 1; i <= pageCount; i++)
            {
                // Calculate a different zoom factor for each page (100%, 120%, 140%, …).
                float zoomFactor = 1.0f + (i - 1) * 0.2f; // 1.0 = 100%

                // -----------------------------------------------------------------
                // 1️⃣ Apply the zoom to the current page using PdfPageEditor.
                // -----------------------------------------------------------------
                string tempPdfPath = Path.Combine(outputDir, $"temp_page_{i}.pdf");
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(doc);
                    editor.ProcessPages = new int[] { i };
                    editor.Zoom = zoomFactor; // e.g., 1.2f = 120%
                    editor.Save(tempPdfPath);
                }

                // -----------------------------------------------------------------
                // 2️⃣ Convert the temporary (zoom‑applied) PDF page to an image.
                // -----------------------------------------------------------------
                using (PdfConverter converter = new PdfConverter())
                {
                    converter.BindPdf(tempPdfPath);
                    converter.StartPage = 1;
                    converter.EndPage   = 1;
                    converter.Resolution = new Resolution(150); // DPI – adjust as needed
                    converter.DoConvert();

                    string outPath = Path.Combine(outputDir,
                        $"Page_{i}_Zoom{(int)(zoomFactor * 100)}.tiff");

                    // SaveAsTIFF is the supported method in Aspose.Pdf.Facades.
                    converter.SaveAsTIFF(outPath);
                }

                // Clean‑up the temporary PDF – it is no longer needed.
                File.Delete(tempPdfPath);

                Console.WriteLine($"Saved page {i} with zoom {zoomFactor * 100}% to '{outputDir}'.");
            }
        }
    }
}
