using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source images that will be merged into a single PDF
        string[] imageFiles = { "image1.jpg", "image2.png", "image3.bmp" };

        // Temporary PDF that will receive the images
        const string tempPdfPath = "temp.pdf";

        // Final output PDF containing all merged images
        const string outputPdfPath = "merged_images.pdf";

        // -----------------------------------------------------------------
        // 1. Create a blank PDF with one page per image.
        // -----------------------------------------------------------------
        using (Document blankDoc = new Document())
        {
            // Add a page for each image (A4 size by default)
            foreach (string _ in imageFiles)
                blankDoc.Pages.Add();

            // Save the blank PDF to a temporary file
            blankDoc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Use PdfFileMend (a Facade) to place each image on its page.
        // -----------------------------------------------------------------
        // The constructor binds the input PDF (tempPdfPath) and the
        // destination PDF (outputPdfPath) in one step.
        PdfFileMend pdfMend = new PdfFileMend(tempPdfPath, outputPdfPath);

        // Coordinates that cover the whole page (A4: 595 x 842 points)
        const float llx = 0f;   // lower‑left X
        const float lly = 0f;   // lower‑left Y
        const float urx = 595f; // upper‑right X
        const float ury = 842f; // upper‑right Y

        // Add each image to its corresponding page (pages are 1‑based)
        for (int i = 0; i < imageFiles.Length; i++)
        {
            string imgPath = imageFiles[i];
            if (!File.Exists(imgPath))
                continue; // Skip missing files

            int pageNumber = i + 1; // Pages start at 1
            pdfMend.AddImage(imgPath, pageNumber, llx, lly, urx, ury);
        }

        // Finalise the operation
        pdfMend.Close();

        // -----------------------------------------------------------------
        // 3. Clean up the temporary PDF.
        // -----------------------------------------------------------------
        if (File.Exists(tempPdfPath))
            File.Delete(tempPdfPath);

        Console.WriteLine($"Images merged into PDF: {outputPdfPath}");
    }
}