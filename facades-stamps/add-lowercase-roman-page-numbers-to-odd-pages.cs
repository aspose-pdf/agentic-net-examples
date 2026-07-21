using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "odd_roman_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int oddPageCounter = 0; // tracks the sequence number for odd pages

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (i % 2 == 1) // process only odd pages
                {
                    oddPageCounter++;

                    // Create a page number stamp
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Use lower‑case Roman numerals
                    stamp.NumberingStyle = NumberingStyle.NumeralsRomanLowercase;

                    // The format must contain '#', which will be replaced by the number
                    stamp.Format = "#";

                    // Set the starting number so that the sequence increments only on odd pages
                    stamp.StartingNumber = oddPageCounter;

                    // Optional: position the stamp at the bottom centre of the page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                    // Apply the stamp to the current page
                    doc.Pages[i].AddStamp(stamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added to odd pages. Output saved as '{outputPath}'.");
    }
}