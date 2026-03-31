using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";
        const string metadataKey = "LandscapePages"; // custom metadata key containing comma‑separated page numbers

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the custom metadata entry (if any)
            string pagesMeta = doc.Info[metadataKey];
            if (string.IsNullOrWhiteSpace(pagesMeta))
            {
                Console.WriteLine("No landscape page metadata found; no rotation applied.");
            }
            else
            {
                // Split the metadata value into individual page numbers
                string[] pageTokens = pagesMeta.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in pageTokens)
                {
                    string trimmed = token.Trim();
                    int pageNumber;
                    if (Int32.TryParse(trimmed, out pageNumber))
                    {
                        if (pageNumber >= 1 && pageNumber <= doc.Pages.Count)
                        {
                            // Rotate the page 90 degrees clockwise
                            doc.Pages[pageNumber].Rotate = Rotation.on90;
                            Console.WriteLine($"Rotated page {pageNumber} by 90 degrees.");
                        }
                        else
                        {
                            Console.WriteLine($"Page number {pageNumber} is out of range; skipped.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid page number '{trimmed}' in metadata; skipped.");
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
            Console.WriteLine($"Document saved as '{outputPath}'.");
        }
    }
}