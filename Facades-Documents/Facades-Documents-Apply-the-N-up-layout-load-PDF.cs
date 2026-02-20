using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class NUpLayout
{
    static void Main(string[] args)
    {
        // Input arguments: source PDF, output PDF, N (pages per sheet)
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: NUpLayout <sourcePdf> <outputPdf> <N-up>");
            return;
        }

        string sourcePath = args[0];
        string outputPath = args[1];
        if (!int.TryParse(args[2], out int nUp) || nUp <= 0)
        {
            Console.WriteLine("Invalid N-up value. It must be a positive integer.");
            return;
        }

        if (!File.Exists(sourcePath))
        {
            Console.WriteLine($"Error: Source file not found – {sourcePath}");
            return;
        }

        try
        {
            // Load source PDF
            Document srcDoc = new Document(sourcePath);
            int srcPageCount = srcDoc.Pages.Count;
            if (srcPageCount == 0)
            {
                Console.WriteLine("Source PDF contains no pages.");
                return;
            }

            // Determine layout (rows x columns) for the given N-up
            int rows = (int)Math.Ceiling(Math.Sqrt(nUp));
            int cols = (int)Math.Ceiling((double)nUp / rows);

            // Use size of the first page as base size
            double pageWidth = srcDoc.Pages[1].PageInfo.Width;
            double pageHeight = srcDoc.Pages[1].PageInfo.Height;

            // Create a new document that will hold the N-up pages
            Document resultDoc = new Document();
            resultDoc.Pages.Add();
            resultDoc.Pages[1].PageInfo.Width = pageWidth * cols;
            resultDoc.Pages[1].PageInfo.Height = pageHeight * rows;

            // Prepare PdfFileMend bound to the result document
            using (PdfFileMend mend = new PdfFileMend(resultDoc))
            using (PdfConverter converter = new PdfConverter(srcDoc))
            {
                // High resolution improves quality of the N-up layout
                converter.Resolution = new Resolution(150);

                // Store each page as an image (TIFF) in memory streams
                List<MemoryStream> pageImages = new List<MemoryStream>();
                for (int i = 1; i <= srcPageCount; i++)
                {
                    converter.StartPage = i;
                    converter.EndPage = i;
                    converter.DoConvert();

                    MemoryStream imgStream = new MemoryStream();
                    // Save the current page as TIFF into the stream (cross‑platform)
                    converter.SaveAsTIFF(imgStream);
                    imgStream.Position = 0; // reset for reading
                    pageImages.Add(imgStream);
                }

                // Place each image onto the single result page according to N-up layout
                for (int i = 0; i < srcPageCount; i++)
                {
                    int col = i % cols;
                    int row = i / cols;

                    // Calculate coordinates for the current cell
                    double llx = col * pageWidth;
                    double lly = resultDoc.Pages[1].PageInfo.Height - ((row + 1) * pageHeight);
                    double urx = (col + 1) * pageWidth;
                    double ury = resultDoc.Pages[1].PageInfo.Height - (row * pageHeight);

                    // Add the image to page 1 of the result document
                    mend.AddImage(pageImages[i], 1, (float)llx, (float)lly, (float)urx, (float)ury);
                }

                // Clean up image streams
                foreach (var ms in pageImages)
                    ms.Dispose();
            }

            // Save the N-up PDF
            resultDoc.Save(outputPath);
            Console.WriteLine($"N-up PDF created successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
