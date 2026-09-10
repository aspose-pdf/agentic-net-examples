using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;                           // core PDF API
using Aspose.Pdf.Facades;                  // facades for extraction and page editing

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_pdfa2b.pdf";

        // ---------------------------------------------------------------
        // Ensure a source PDF exists (self‑contained example). If the file
        // does not exist we create a minimal one‑page PDF so the extractor
        // has something to bind to.
        // ---------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        // -----------------------------------------------------------------
        // 1. Extract all images from the source PDF into temporary PNG files
        // -----------------------------------------------------------------
        string tempImageDir = Path.Combine(Path.GetTempPath(), "AsposeExtractedImages");
        Directory.CreateDirectory(tempImageDir);

        var extractedImageFiles = new List<string>();
        PdfExtractor extractor = new PdfExtractor();

        extractor.BindPdf(inputPdfPath);          // bind source PDF
        extractor.ExtractImage();                 // start image extraction

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            // Save each image using its original format (no System.Drawing needed)
            string imagePath = Path.Combine(tempImageDir, $"image_{imageIndex}.png");
            extractor.GetNextImage(imagePath);   // overload without ImageFormat avoids CA1416 warning
            extractedImageFiles.Add(imagePath);
            imageIndex++;
        }
        extractor.Close();                       // release resources

        // ---------------------------------------------------------------
        // 2. Create a new PDF document and embed the extracted images
        // ---------------------------------------------------------------
        using (Document newDoc = new Document())
        {
            // Add the first page (additional pages will be added as needed)
            newDoc.Pages.Add();

            // PdfFileMend allows adding images to pages as XObjects
            PdfFileMend mend = new PdfFileMend(newDoc);

            int currentPage = 1;
            float posX = 50f;                     // left margin
            float posY = 800f;                    // start from top of the page
            const float verticalStep = 200f;      // space between images

            foreach (string imgFile in extractedImageFiles)
            {
                // Add the image to the current page at the specified coordinates
                // Width and height set to 0 keep the original image dimensions.
                mend.AddImage(imgFile, currentPage, posX, posY, 0, 0);

                // Update Y position for the next image
                posY -= verticalStep;

                // If we run out of space, create a new page
                if (posY < 100f)
                {
                    newDoc.Pages.Add();
                    currentPage++;
                    posY = 800f;
                }
            }

            // -----------------------------------------------------------
            // 3. Convert the document to PDF/A‑2b compliance
            // -----------------------------------------------------------
            string conversionLog = Path.Combine(tempImageDir, "conversion.log");
            newDoc.Convert(conversionLog, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

            // Save the final PDF/A‑2b document
            newDoc.Save(outputPdfPath);
        }

        // ---------------------------------------------------------------
        // 4. Clean up temporary image files and directory
        // ---------------------------------------------------------------
        foreach (string file in extractedImageFiles)
        {
            try { File.Delete(file); } catch { /* ignore */ }
        }
        try { Directory.Delete(tempImageDir, true); } catch { /* ignore */ }

        Console.WriteLine($"PDF/A‑2b document created at '{outputPdfPath}'.");
    }
}
