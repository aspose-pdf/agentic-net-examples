using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class InsertImageIntoGraph
{
    static void Main()
    {
        const string imagePath   = "sample.png";   // external image file
        const string outputPath  = "result.pdf";   // output PDF
        const double targetWidth = 200;            // desired width (points)
        const float  rotateAngle = 45f;            // rotation angle in degrees

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Load the image as an XImage to obtain its original dimensions
            string imgName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                imgName = page.Resources.Images.Add(imgStream);
            }

            XImage xImg = page.Resources.Images[imgName];
            double originalWidth  = xImg.Width;
            double originalHeight = xImg.Height;

            // Compute scaling factor to achieve the target width while preserving aspect ratio
            double scaleFactor = targetWidth / originalWidth;
            double targetHeight = originalHeight * scaleFactor;

            // Calculate rectangle to position the image at the centre of the page
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double llx = (pageWidth  - targetWidth)  / 2;
            double lly = (pageHeight - targetHeight) / 2;
            double urx = llx + targetWidth;
            double ury = lly + targetHeight;

            // Use ImageStamp to place the image with scaling and rotation
            ImageStamp imgStamp = new ImageStamp(imagePath);
            imgStamp.Width       = (float)targetWidth;   // set explicit width
            imgStamp.Height      = (float)targetHeight; // set explicit height (preserves proportion)
            imgStamp.RotateAngle = rotateAngle;          // arbitrary rotation angle
            imgStamp.XIndent     = (float)llx;           // horizontal position
            imgStamp.YIndent     = (float)lly;           // vertical position
            imgStamp.Background  = false;               // stamp on top of page content

            // Add the stamp to the page
            page.AddStamp(imgStamp);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}