using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Create a stamp with a format that includes the page number placeholder '#'
                PageNumberStamp stamp = new PageNumberStamp("Page #");
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Bottom;
                stamp.BottomMargin = 20f; // distance from the bottom edge
                page.AddStamp(stamp);
            }

            // Saving may require GDI+ on non‑Windows platforms; guard the call.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping save on non‑Windows platform due to possible GDI+ requirement.");
            }
        }
    }
}