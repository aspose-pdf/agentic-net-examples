using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to inspect custom metadata.
        using (Document doc = new Document(inputPath))
        {
            // Determine whether rotation is required based on a custom property stored in the document's Info dictionary.
            bool rotatePages = false;
            string flag = doc.Info["Landscape"]; // Retrieve custom metadata via the indexer.
            if (!string.IsNullOrEmpty(flag))
            {
                rotatePages = string.Equals(flag, "true", StringComparison.OrdinalIgnoreCase);
            }

            if (rotatePages)
            {
                // Rotate every page 90 degrees clockwise using the Page.Rotate property.
                for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
                {
                    doc.Pages[i].Rotate = Rotation.on90; // on90 = 90° clockwise
                }
            }

            // Save the (possibly rotated) PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
