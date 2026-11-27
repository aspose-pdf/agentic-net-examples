using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesFolder = "Page3Images";
        const string manifestPath = "manifest.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(imagesFolder);

        // Load the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Verify that page 3 exists (page-indexing-one-based rule)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            // Access page 3 (1‑based indexing)
            Page page = doc.Pages[3];

            var jpegFileNames = new List<string>();
            int imageIndex = 1;

            // Iterate over images on the page
            foreach (XImage img in page.Resources.Images)
            {
                // Save image to a memory stream first
                using (var ms = new MemoryStream())
                {
                    img.Save(ms);
                    byte[] data = ms.ToArray();

                    // Detect JPEG by inspecting the first two bytes (0xFF 0xD8)
                    if (IsJpeg(data))
                    {
                        string fileName = $"image_{imageIndex:D3}.jpg";
                        string filePath = System.IO.Path.Combine(imagesFolder, fileName);

                        // Write the JPEG bytes to disk
                        File.WriteAllBytes(filePath, data);

                        jpegFileNames.Add(fileName);
                        imageIndex++;
                    }
                }
            }

            // Write the manifest file listing the JPEG filenames
            using (StreamWriter writer = new StreamWriter(manifestPath, false))
            {
                foreach (string name in jpegFileNames)
                {
                    writer.WriteLine(name);
                }
            }

            Console.WriteLine($"Extracted {jpegFileNames.Count} JPEG image(s) from page 3.");
            Console.WriteLine($"Manifest written to: {manifestPath}");
        }
    }

    // Helper method to identify JPEG files via header bytes
    private static bool IsJpeg(byte[] data)
    {
        return data != null && data.Length >= 2 && data[0] == 0xFF && data[1] == 0xD8;
    }
}
