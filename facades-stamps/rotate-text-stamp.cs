using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Create a text stamp and rotate it 45 degrees around its centre.
            TextStamp stamp = new TextStamp("CONFIDENTIAL");
            stamp.RotateAngle = 45f; // Correct property for rotation
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;
            stamp.Opacity = 0.5f;

            // Apply the stamp to every page.
            foreach (Page page in document.Pages)
            {
                page.AddStamp(stamp);
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Stamp added and rotated 45 degrees. Saved to '{outputPath}'.");
    }
}
