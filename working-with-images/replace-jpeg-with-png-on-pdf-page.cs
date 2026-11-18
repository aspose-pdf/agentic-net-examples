using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string pngPath = "high_res_image.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(pngPath))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {pngPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3];

            // Keep the replacement streams alive until the document is saved
            List<MemoryStream> replacementStreams = new List<MemoryStream>();

            // Iterate over the image resources on page 3 using an index because the collection is read‑only for direct assignment
            for (int i = 1; i <= page.Resources.Images.Count; i++) // XImageCollection is 1‑based
            {
                XImage img = page.Resources.Images[i];

                // Identify the JPEG image by its name/extension (fallback when ImageInfo is unavailable)
                if (!string.IsNullOrEmpty(img.Name) &&
                    (img.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                     img.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)))
                {
                    // Load the PNG bytes
                    byte[] pngBytes = File.ReadAllBytes(pngPath);
                    // Create a memory stream that will survive until after Save()
                    MemoryStream ms = new MemoryStream(pngBytes);

                    // Replace the image data using the collection's Replace method
                    page.Resources.Images.Replace(i, ms);

                    // Update the image name so downstream tools recognise the new format
                    page.Resources.Images[i].Name = Path.GetFileName(pngPath);

                    // Store the stream to prevent it from being disposed prematurely
                    replacementStreams.Add(ms);

                    Console.WriteLine("JPEG image replaced with PNG.");
                    break; // Assuming only one JPEG needs replacement
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);

            // Dispose the temporary streams after the document has been saved
            foreach (var s in replacementStreams)
                s.Dispose();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
