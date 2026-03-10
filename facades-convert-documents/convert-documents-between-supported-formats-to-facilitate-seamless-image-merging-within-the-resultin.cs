using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                     // Core PDF API (Document, Image, Page)
using Aspose.Pdf.Facades;            // Facade API for merging (PdfFileEditor)

class ImageToPdfMerger
{
    /// <summary>
    /// Converts a collection of image files (any format supported by Aspose.Pdf.Image)
    /// into individual PDF streams and merges them into a single PDF file.
    /// </summary>
    /// <param name="imagePaths">Full paths of the source images.</param>
    /// <param name="outputPdfPath">Full path where the merged PDF will be saved.</param>
    public static void ConvertAndMerge(string[] imagePaths, string outputPdfPath)
    {
        if (imagePaths == null || imagePaths.Length == 0)
            throw new ArgumentException("No image files provided.", nameof(imagePaths));

        // List to hold PDF streams generated from each image.
        List<Stream> pdfStreams = new List<Stream>();

        try
        {
            // Convert each image to a one‑page PDF stored in a MemoryStream.
            foreach (string imgPath in imagePaths)
            {
                if (!File.Exists(imgPath))
                    throw new FileNotFoundException($"Image file not found: {imgPath}");

                // Create a new PDF document.
                using (Document imgPdf = new Document())
                {
                    // Add a page and place the image on it.
                    Page page = imgPdf.Pages.Add();
                    Aspose.Pdf.Image img = new Aspose.Pdf.Image
                    {
                        File = imgPath
                    };
                    page.Paragraphs.Add(img);

                    // Save the PDF to a memory stream (keep it open for later merging).
                    MemoryStream ms = new MemoryStream();
                    imgPdf.Save(ms);
                    ms.Position = 0;               // Reset position for reading.
                    pdfStreams.Add(ms);
                }
                // Note: the Document is disposed here, but the MemoryStream remains valid.
            }

            // Prepare the output stream for the final merged PDF.
            using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
            {
                // Use PdfFileEditor (Facades) to concatenate the PDF streams.
                PdfFileEditor editor = new PdfFileEditor();
                editor.Concatenate(pdfStreams.ToArray(), outputStream);
            }
        }
        finally
        {
            // Ensure all intermediate streams are properly disposed.
            foreach (Stream s in pdfStreams)
                s.Dispose();
        }
    }

    // Example usage.
    static void Main()
    {
        string[] images = {
            "C:\\Images\\photo1.jpg",
            "C:\\Images\\diagram.png",
            "C:\\Images\\chart.bmp"
        };

        string mergedPdf = "C:\\Output\\merged_images.pdf";

        try
        {
            ConvertAndMerge(images, mergedPdf);
            Console.WriteLine($"Merged PDF created at: {mergedPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}