using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Drawing; // for Drawing.Rectangle

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string reportPdf = "report.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(sourcePdf))
        {
            // Locate the first HighlightAnnotation in the document and keep its page reference
            HighlightAnnotation highlight = null;
            Page highlightPage = null;
            foreach (Page page in srcDoc.Pages)
            {
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is HighlightAnnotation ha)
                    {
                        highlight = ha;
                        highlightPage = page;
                        break;
                    }
                }
                if (highlight != null) break;
            }

            if (highlight == null || highlightPage == null)
            {
                Console.WriteLine("No highlight annotation found in the source PDF.");
                return;
            }

            // Render the area covered by the highlight annotation to an image stream
            using (MemoryStream imgStream = new MemoryStream())
            {
                // The rectangle of the annotation (in PDF user units)
                Aspose.Pdf.Rectangle pdfRect = highlight.Rect;

                // Preserve the original CropBox so we can restore it later
                var originalCropBox = highlightPage.CropBox;
                // Set the page's CropBox to the annotation rectangle – this limits the rendering area
                highlightPage.CropBox = new Aspose.Pdf.Rectangle(pdfRect.LLX, pdfRect.LLY, pdfRect.URX, pdfRect.URY);

                // Create a PNG device. Use CropBox coordinate type so the device respects the page's CropBox.
                var pngDevice = new PngDevice
                {
                    CoordinateType = PageCoordinateType.CropBox
                };
                // Optional: set resolution if higher quality is required
                // pngDevice.Resolution = new Resolution(300);

                try
                {
                    // Render the (cropped) page to the image stream
                    pngDevice.Process(highlightPage, imgStream);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to render annotation preview: {ex.Message}");
                    // Restore original CropBox before exiting
                    highlightPage.CropBox = originalCropBox;
                    return;
                }
                finally
                {
                    // Restore the original CropBox regardless of success/failure
                    highlightPage.CropBox = originalCropBox;
                }

                imgStream.Position = 0; // Reset stream position for reading

                // Create a new PDF report and embed the rendered image
                using (Document reportDoc = new Document())
                {
                    // Add a blank page to the report
                    Page reportPage = reportDoc.Pages.Add();

                    // Create an ImageStamp from the image stream
                    ImageStamp stamp = new ImageStamp(imgStream)
                    {
                        // Center the image on the page
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Center,

                        // Preserve the original annotation size (PDF units are points)
                        Width  = pdfRect.Width,
                        Height = pdfRect.Height
                    };

                    // Add the stamp to the report page
                    reportPage.AddStamp(stamp);

                    // Save the report PDF
                    reportDoc.Save(reportPdf);
                }
            }
        }

        Console.WriteLine($"Report generated successfully: {reportPdf}");
    }
}
