using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the source PDF, preserve its XMP metadata, add an image stamp, then save.
        using (Document doc = new Document(inputPdfPath))
        {
            // Preserve existing XMP metadata (if any).
            MemoryStream originalXmp = new MemoryStream();
            doc.GetXmpMetadata(originalXmp);
            // Reset stream position for later use.
            originalXmp.Position = 0;

            // Create an image stamp.
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                Opacity = 0.7,
                Background = false
            };

            // Apply the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Restore the original XMP metadata before saving (if any was present).
            if (originalXmp.Length > 0)
            {
                originalXmp.Position = 0; // ensure we start from the beginning
                doc.SetXmpMetadata(originalXmp);
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and XMP metadata retained. Output saved to '{outputPdfPath}'.");
    }
}
