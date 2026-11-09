using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagePath = "stampImage.png";
        const string outputPdf = "output.pdf";
        const string tempPdf = "temp_with_attachment.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add the image file as an embedded file (attachment) using the Document API.
        // ------------------------------------------------------------
        using (var doc = new Document(inputPdf))
        {
            // Create a FileSpecification that represents the embedded file.
            var fileSpec = new FileSpecification(Path.GetFileName(imagePath))
            {
                Description = "Sample image attachment"
            };
            // Set the file data via a stream.
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(imagePath));
            // Optional: set modification date.
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Add the file specification to the EmbeddedFiles collection.
            doc.EmbeddedFiles.Add(fileSpec);

            // Save to a temporary PDF that now contains the embedded file.
            doc.Save(tempPdf);
        }

        // ------------------------------------------------------------
        // 2. Create an ImageStamp that references the same image file.
        // ------------------------------------------------------------
        var imgStamp = new ImageStamp(imagePath)
        {
            // Size of the stamp.
            Width = 200,
            Height = 150,
            // Place the stamp in the foreground.
            Background = false,
            // Use absolute positioning.
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
            XIndent = 100, // distance from the left edge (points)
            YIndent = 500  // distance from the bottom edge (points)
        };

        // ------------------------------------------------------------
        // 3. Apply the stamp only to the first page of the PDF that already contains the attachment.
        // ------------------------------------------------------------
        using (var doc = new Document(tempPdf))
        {
            // Add the stamp to page 1.
            doc.Pages[1].AddStamp(imgStamp);
            doc.Save(outputPdf);
        }

        // Clean‑up the intermediate file (optional).
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"PDF saved with attachment and stamp: {outputPdf}");
    }
}
