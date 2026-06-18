using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Collect custom page dimensions from the user (points)
        Console.Write("Enter page width (points): ");
        if (!double.TryParse(Console.ReadLine(), out double width) || width <= 0)
        {
            Console.Error.WriteLine("Invalid width.");
            return;
        }

        Console.Write("Enter page height (points): ");
        if (!double.TryParse(Console.ReadLine(), out double height) || height <= 0)
        {
            Console.Error.WriteLine("Invalid height.");
            return;
        }

        // Collect insertion position (1‑based indexing)
        Console.Write("Enter insertion position (1 = first page): ");
        if (!int.TryParse(Console.ReadLine(), out int position) || position < 1)
        {
            Console.Error.WriteLine("Invalid position.");
            return;
        }

        try
        {
            // Load existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Clamp position to a valid range (1 .. Pages.Count+1)
                int insertPos = Math.Min(position, doc.Pages.Count + 1);

                // Insert an empty page at the desired position
                Page newPage = doc.Pages.Insert(insertPos);

                // Apply the user‑defined size
                newPage.SetPageSize(width, height);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Empty page inserted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}