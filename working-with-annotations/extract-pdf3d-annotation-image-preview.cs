using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "source.pdf";      // PDF containing the annotation
        const string outputPdfPath = "report.pdf";      // PDF report with the extracted image

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the source PDF
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Iterate through pages and annotations to find a PDF3DAnnotation.
            // (HighlightAnnotation does not provide an image preview, so we use PDF3DAnnotation as an example.)
            foreach (Page srcPage in sourceDoc.Pages)
            {
                foreach (Annotation ann in srcPage.Annotations)
                {
                    if (ann is PDF3DAnnotation pdf3dAnn)
                    {
                        // Retrieve the preview image as a stream.
                        using (Stream previewStream = pdf3dAnn.GetImagePreview())
                        {
                            // Create a new PDF document that will serve as the report.
                            using (Document reportDoc = new Document())
                            {
                                // Add a page to the report.
                                Page reportPage = reportDoc.Pages.Add();

                                // Define where the image will be placed on the report page.
                                // Rectangle(left, bottom, right, top) – coordinates are in points.
                                Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

                                // Add the image to the report page directly from the stream.
                                // The overload AddImage(Stream, Rectangle) embeds the image.
                                reportPage.AddImage(previewStream, imageRect);

                                // Save the report PDF.
                                reportDoc.Save(outputPdfPath);
                            }
                        }

                        // Image extracted – exit loops.
                        Console.WriteLine($"Image preview extracted and saved to '{outputPdfPath}'.");
                        return;
                    }
                }
            }

            Console.WriteLine("No PDF3DAnnotation with an image preview was found in the document.");
        }
    }
}