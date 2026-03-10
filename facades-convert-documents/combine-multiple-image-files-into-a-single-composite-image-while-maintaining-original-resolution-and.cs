using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing; // Aspose image format enum

class Program
{
    static void Main()
    {
        // Input image files to be merged
        string[] imageFiles = { "image1.png", "image2.jpg", "image3.tif" };
        // Output merged image file
        const string outputImagePath = "merged_output.png";

        // Validate input files exist
        foreach (string path in imageFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Prepare list of image streams
        List<Stream> imageStreams = new List<Stream>();
        try
        {
            foreach (string path in imageFiles)
            {
                // Open each image as a read‑only FileStream
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                imageStreams.Add(fs);
            }

            // Choose output format – use the format of the first image for consistency
            Aspose.Pdf.Drawing.ImageFormat outputFormat = GetAsposeImageFormat(System.IO.Path.GetExtension(imageFiles[0]));

            // Merge mode – Center keeps original resolution without scaling
            ImageMergeMode mergeMode = ImageMergeMode.Center;

            // Horizontal and vertical ratios are optional; null lets the method use default canvas size
            int? horizontalRatio = null;
            int? verticalRatio = null;

            // Perform the merge; result is a stream containing the merged image
            Stream mergedStream = PdfConverter.MergeImages(
                imageStreams,
                outputFormat,
                mergeMode,
                horizontalRatio,
                verticalRatio);

            // Write the merged image stream to the output file
            using (FileStream outFile = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
            {
                mergedStream.CopyTo(outFile);
            }

            Console.WriteLine($"Merged image saved to '{outputImagePath}'.");
        }
        finally
        {
            // Ensure all opened image streams are closed
            foreach (Stream s in imageStreams)
            {
                s.Dispose();
            }
        }
    }

    // Helper to map file extension to Aspose.Pdf.Drawing.ImageFormat
    private static Aspose.Pdf.Drawing.ImageFormat GetAsposeImageFormat(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".png":
                return Aspose.Pdf.Drawing.ImageFormat.Png;
            case ".jpg":
            case ".jpeg":
                return Aspose.Pdf.Drawing.ImageFormat.Jpeg;
            case ".tif":
            case ".tiff":
                return Aspose.Pdf.Drawing.ImageFormat.Tiff;
            case ".bmp":
                return Aspose.Pdf.Drawing.ImageFormat.Bmp;
            default:
                // Default to PNG if unknown
                return Aspose.Pdf.Drawing.ImageFormat.Png;
        }
    }
}