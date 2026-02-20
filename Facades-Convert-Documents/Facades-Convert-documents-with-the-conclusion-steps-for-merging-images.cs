using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class MergePdfImages
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output merged image file path (TIFF)
        const string mergedImagePath = "merged.tiff";

        // Validate input file
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // List to hold image streams for each page
        List<Stream> pageImages = new List<Stream>();

        try
        {
            // Initialize PdfConverter and bind the PDF document
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(pdfPath);

                // Optional: set page range (convert all pages)
                converter.StartPage = 1;
                converter.EndPage = converter.PageCount;

                // Prepare for conversion
                converter.DoConvert();

                // Extract images page by page
                while (converter.HasNextImage())
                {
                    // Get image as a stream (default format is JPEG)
                    MemoryStream imgStream = new MemoryStream();
                    converter.GetNextImage(imgStream);
                    imgStream.Position = 0; // reset for later reading
                    pageImages.Add(imgStream);
                }

                // Close the converter (releases resources)
                converter.Close();
            }

            // Merge all page images into a single multi‑frame TIFF
            using (Stream mergedStream = PdfConverter.MergeImagesAsTiff(pageImages))
            {
                // Ensure the stream is positioned at the beginning
                mergedStream.Position = 0;

                // Save the merged TIFF to disk
                using (FileStream fileOut = new FileStream(mergedImagePath, FileMode.Create, FileAccess.Write))
                {
                    mergedStream.CopyTo(fileOut);
                }
            }

            Console.WriteLine($"Merged image saved successfully to '{mergedImagePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Dispose all individual page image streams
            foreach (var s in pageImages)
            {
                s.Dispose();
            }
        }
    }
}