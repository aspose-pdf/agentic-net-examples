using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_even_duration.pdf";
        const int duration = 5; // seconds for even pages

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page display duration
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Total number of pages in the document
            int totalPages = editor.GetPages();

            // Apply duration only to even‑numbered pages
            for (int pageNum = 2; pageNum <= totalPages; pageNum += 2)
            {
                // Specify which page to edit – ProcessPages expects an int[]
                editor.ProcessPages = new int[] { pageNum };

                // Set the desired display duration (in seconds)
                editor.DisplayDuration = duration;

                // Apply the change to the specified page
                editor.ApplyChanges();
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with even‑page durations to '{outputPath}'.");
    }
}
