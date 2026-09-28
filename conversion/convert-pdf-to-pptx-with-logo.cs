using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, temporary PPTX (after conversion), final PPTX (with logo), and logo image paths
        const string inputPdfPath   = "input.pdf";
        const string tempPptxPath   = "temp_output.pptx";
        const string finalPptxPath  = "output_with_logo.pptx";
        const string logoImagePath  = "company_logo.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // -------------------------------------------------
        // 1. Load PDF and stamp the logo on every page
        // -------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Logo dimensions (points) and margins
            const float logoWidth   = 100f; // width of logo
            const float logoHeight  = 50f;  // height of logo
            const float marginRight = 20f; // distance from right edge
            const float marginTop   = 20f; // distance from top edge

            foreach (Page page in pdfDoc.Pages)
            {
                // Create an image stamp for the logo
                ImageStamp logoStamp = new ImageStamp(logoImagePath)
                {
                    Width  = logoWidth,
                    Height = logoHeight,
                    // Align to the top‑right corner
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Offsets from the chosen edges
                    XIndent = marginRight,
                    YIndent = marginTop,
                    // Ensure the stamp is placed over page content
                    Background = false
                };

                // Add the stamp to the current page
                page.AddStamp(logoStamp);
            }

            // -------------------------------------------------
            // 2. Convert the stamped PDF to PPTX
            // -------------------------------------------------
            var pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(tempPptxPath, pptxOptions);
        }

        // -------------------------------------------------
        // 3. The PPTX already contains the logo (added before conversion).
        //    If you still need to rename or move the file, do it here.
        // -------------------------------------------------
        try
        {
            // Move the temporary PPTX to the final destination name
            if (File.Exists(finalPptxPath))
                File.Delete(finalPptxPath);
            File.Move(tempPptxPath, finalPptxPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error finalising PPTX: {ex.Message}");
            // If moving fails, keep the temporary file for debugging
        }

        Console.WriteLine($"Conversion complete. PPTX with logo saved to '{finalPptxPath}'.");
    }
}
