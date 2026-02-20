using System;
using System.IO;
using Aspose.Pdf.Facades;

class CombineImagesToPdf
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: output PDF path followed by one or more image file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: CombineImagesToPdf <output.pdf> <image1> [<image2> ...]");
            return;
        }

        string outputPdfPath = args[0];
        string[] imagePaths = new string[args.Length - 1];
        Array.Copy(args, 1, imagePaths, 0, imagePaths.Length);

        // Verify that all image files exist before proceeding
        foreach (string imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Error: Image file not found – {imgPath}");
                return;
            }
        }

        try
        {
            // PdfFileMend is a facade that allows adding images directly to a PDF document
            using (PdfFileMend pdfMend = new PdfFileMend())
            {
                // Define a page size (A4) in points: 595 x 842
                const float pageWidth = 595f;
                const float pageHeight = 842f;

                // Add each image on a separate page
                for (int i = 0; i < imagePaths.Length; i++)
                {
                    int pageNumber = i + 1; // Pages are 1‑based
                    string imgPath = imagePaths[i];

                    // AddImage(string imagePath, int pageNumber, float llx, float lly, float urx, float ury)
                    // The rectangle defined by (llx,lly)-(urx,ury) covers the whole page,
                    // causing the image to be scaled to fit the page.
                    pdfMend.AddImage(imgPath, pageNumber, 0f, 0f, pageWidth, pageHeight);
                }

                // Save the resulting PDF to the specified output file
                pdfMend.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully created PDF '{outputPdfPath}' with {imagePaths.Length} page(s).");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while creating the PDF: {ex.Message}");
        }
    }
}