using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf"; // result PDF
        const string dicomImage = "image.dcm"; // DICOM image to embed

        // Desired size of the image on the page (points)
        double customWidth = 200; // e.g., 200 points
        double customHeight = 150; // e.g., 150 points

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(dicomImage))
        {
            Console.Error.WriteLine($"DICOM image not found: {dicomImage}");
            return;
        }

        // Load the PDF (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Use the first page; add a new page if the document is empty
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create an ImageStamp, set custom dimensions and position, then add it to the page
            ImageStamp imgStamp = new ImageStamp(dicomImage);
            imgStamp.Width = (float)customWidth;
            imgStamp.Height = (float)customHeight;
            // Position the stamp – XIndent = distance from the left edge, YIndent = distance from the bottom edge
            imgStamp.XIndent = 50;   // X coordinate (points)
            imgStamp.YIndent = 500; // Y coordinate (points)

            page.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"DICOM image inserted and saved to '{outputPdf}'.");
    }
}
