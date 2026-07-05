using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath  = "output.pdf";

        // Verify required files exist
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

        // Load the source PDF, extract its XMP metadata, add the image stamp,
        // then re‑apply the metadata before saving.
        using (Document doc = new Document(inputPdfPath))
        {
            // Extract existing XMP metadata into a memory stream
            using (MemoryStream xmpStream = new MemoryStream())
            {
                doc.GetXmpMetadata(xmpStream);
                xmpStream.Position = 0; // Reset for later reading

                // Create an image stamp from the specified image file
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    // Example visual settings (optional)
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Opacity             = 0.5f
                };

                // Apply the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Re‑apply the original XMP metadata to the modified document
                doc.SetXmpMetadata(xmpStream);
            }

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}