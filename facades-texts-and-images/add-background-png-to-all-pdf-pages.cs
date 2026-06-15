using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // PDF with background image
        const string bgImage   = "background.png"; // PNG to be added as background

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(bgImage))
        {
            Console.Error.WriteLine($"Background image not found: {bgImage}");
            return;
        }

        // PdfFileMend is a Facades class that allows adding images to existing PDFs.
        // Use a using block for deterministic disposal.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF file.
            mend.BindPdf(inputPdf);

            // Access the underlying Document to obtain page dimensions.
            Document doc = mend.Document;

            // Iterate through all pages (1‑based indexing).
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Use the page rectangle to determine full‑page coordinates.
                // Lower‑left corner is (0,0); upper‑right corner is (width, height).
                float lowerLeftX  = 0f;
                float lowerLeftY  = 0f;
                float upperRightX = (float)page.Rect.Width;
                float upperRightY = (float)page.Rect.Height;

                // Add the PNG image to the current page, covering the whole page.
                // The AddImage method returns a bool indicating success; ignore for brevity.
                mend.AddImage(bgImage, pageNum, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            }

            // Save the modified PDF to the output path.
            mend.Save(outputPdf);

            // Close the facade (releases internal resources).
            mend.Close();
        }

        Console.WriteLine($"Background image added to all pages. Output saved as '{outputPdf}'.");
    }
}