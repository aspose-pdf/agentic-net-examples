using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";
        const int pageNumber = 1; // 1‑based page index
        const Rotation rotation = Rotation.on90; // 90° clockwise rotation

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdf = new Document(inputPath))
            {
                // Apply rotation to the specified page
                pdf.Pages[pageNumber].Rotate = rotation;

                // Retrieve the page size after rotation
                PageInfo pageInfo = pdf.Pages[pageNumber].PageInfo;
                Console.WriteLine($"After rotation (page {pageNumber}): Width = {pageInfo.Width}, Height = {pageInfo.Height}");

                // Save the rotated document (optional)
                pdf.Save(outputPath);
                Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
