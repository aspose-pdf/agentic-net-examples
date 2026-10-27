using System;
using System.IO;
using System.Linq;
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

        // Load the PDF document.
        using (Document pdf = new Document(inputPath))
        {
            // Retrieve custom metadata that lists pages needing rotation.
            // Expected custom property: "LandscapePages" = "1,3,5"
            string propValue = pdf.Info["LandscapePages"] as string;

            if (string.IsNullOrWhiteSpace(propValue))
            {
                // No pages flagged for rotation – just copy the original file.
                File.Copy(inputPath, outputPath, true);
                Console.WriteLine("No rotation needed. File copied unchanged.");
                return;
            }

            // Parse the comma‑separated list of page numbers.
            var pagesToRotate = propValue
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => int.Parse(p.Trim()))
                .ToList();

            // Rotate each specified page 90 degrees clockwise.
            foreach (int pageNumber in pagesToRotate)
            {
                if (pageNumber < 1 || pageNumber > pdf.Pages.Count)
                {
                    Console.WriteLine($"Warning: page {pageNumber} is out of range and will be ignored.");
                    continue;
                }

                // Aspose.Pdf uses the Rotation enum. on90 = 90° clockwise.
                pdf.Pages[pageNumber].Rotate = Rotation.on90;
            }

            // Save the modified PDF.
            pdf.Save(outputPath);
            Console.WriteLine($"Rotated pages saved to '{outputPath}'.");
        }
    }
}
