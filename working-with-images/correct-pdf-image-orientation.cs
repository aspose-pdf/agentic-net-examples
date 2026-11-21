using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    // Map EXIF orientation values to System.Drawing.RotateFlipType
    static System.Drawing.RotateFlipType GetRotateFlipType(int orientation)
    {
        switch (orientation)
        {
            case 2: return System.Drawing.RotateFlipType.RotateNoneFlipX;
            case 3: return System.Drawing.RotateFlipType.Rotate180FlipNone;
            case 4: return System.Drawing.RotateFlipType.Rotate180FlipX;
            case 5: return System.Drawing.RotateFlipType.Rotate90FlipX;
            case 6: return System.Drawing.RotateFlipType.Rotate90FlipNone;
            case 7: return System.Drawing.RotateFlipType.Rotate270FlipX;
            case 8: return System.Drawing.RotateFlipType.Rotate270FlipNone;
            default: return System.Drawing.RotateFlipType.RotateNoneFlipNone; // 1 or unknown
        }
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_corrected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF and process each embedded image
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // XImageCollection uses a 1‑based index for Replace()
                int imgIdx = 1;
                foreach (Aspose.Pdf.XImage pdfImg in page.Resources.Images)
                {
                    // Export the current image to a memory stream
                    using (MemoryStream srcStream = new MemoryStream())
                    {
                        pdfImg.Save(srcStream);
                        srcStream.Position = 0;

                        // Load with System.Drawing to read EXIF orientation
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(srcStream))
                        {
                            const int orientationTag = 274; // EXIF orientation tag ID
                            if (sysImg.PropertyIdList.Contains(orientationTag))
                            {
                                System.Drawing.Imaging.PropertyItem prop = sysImg.GetPropertyItem(orientationTag);
                                int orientation = BitConverter.ToUInt16(prop.Value, 0);

                                if (orientation != 1) // 1 = normal orientation
                                {
                                    // Rotate/flip according to EXIF data
                                    System.Drawing.RotateFlipType rotateFlip = GetRotateFlipType(orientation);
                                    sysImg.RotateFlip(rotateFlip);

                                    // Save the corrected image to a new stream
                                    using (MemoryStream correctedStream = new MemoryStream())
                                    {
                                        System.Drawing.Imaging.ImageFormat fmt = sysImg.RawFormat;
                                        sysImg.Save(correctedStream, fmt);
                                        correctedStream.Position = 0;

                                        // Replace the image in the PDF page resources
                                        page.Resources.Images.Replace(imgIdx, correctedStream);
                                    }
                                }
                            }
                        }
                    }
                    imgIdx++;
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
