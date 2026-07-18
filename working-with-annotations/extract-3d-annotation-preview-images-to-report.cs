using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF containing annotations
        const string reportPdfPath = "report.pdf";        // output PDF with extracted images

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the source document
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Create a new document that will serve as the report
            using (Document reportDoc = new Document())
            {
                // Add a first blank page to the report
                reportDoc.Pages.Add();

                // Keep track of where to place extracted images on the report page
                double cursorY = reportDoc.Pages[1].PageInfo.Height - 50; // start near top
                const double marginX = 50;
                const double imageWidth = 200;
                const double imageHeight = 200;
                const double verticalSpacing = 20;

                // Iterate through all pages of the source PDF
                for (int pageIdx = 1; pageIdx <= srcDoc.Pages.Count; pageIdx++)
                {
                    Page srcPage = srcDoc.Pages[pageIdx];

                    // Iterate over annotations on the current page
                    foreach (Annotation ann in srcPage.Annotations)
                    {
                        // We're interested in PDF3DAnnotation (has an image preview)
                        if (ann is PDF3DAnnotation pdf3dAnn)
                        {
                            // Retrieve the preview image as a stream
                            Stream previewStream = pdf3dAnn.GetImagePreview();

                            if (previewStream != null && previewStream.Length > 0)
                            {
                                // Define the rectangle where the image will be placed on the report page
                                Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(
                                    marginX,
                                    cursorY - imageHeight,
                                    marginX + imageWidth,
                                    cursorY);

                                // Add the image directly to the report page
                                // The AddImage method consumes the stream and places the image at the given rectangle
                                reportDoc.Pages[1].AddImage(previewStream, imgRect);

                                // Move the cursor down for the next image
                                cursorY -= (imageHeight + verticalSpacing);

                                // If we run out of vertical space, add a new page
                                if (cursorY < 50)
                                {
                                    reportDoc.Pages.Add();
                                    cursorY = reportDoc.Pages[reportDoc.Pages.Count].PageInfo.Height - 50;
                                }
                            }
                        }
                    }
                }

                // Save the generated report PDF
                reportDoc.Save(reportPdfPath);
                Console.WriteLine($"Report with extracted annotation images saved to '{reportPdfPath}'.");
            }
        }
    }
}