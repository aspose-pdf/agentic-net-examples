using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "slideshow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define display durations (in seconds) for pages.
        // Index 0 => page 1, index 1 => page 2, etc.
        // Pages beyond the array will use a default duration of 3 seconds.
        int[] pageDurations = { 5, 10, 3 };

        // Use PdfPageEditor (a Facade) to edit page properties.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Total number of pages in the document.
            int totalPages = editor.GetPages();

            // Set the display duration for each page individually.
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                int duration = (pageNumber <= pageDurations.Length) ? pageDurations[pageNumber - 1] : 3;

                // Restrict editing to the current page.
                editor.ProcessPages = new int[] { pageNumber };

                // Assign the desired duration (seconds).
                editor.DisplayDuration = duration;

                // Apply the change to the document.
                editor.ApplyChanges();
            }

            // Save the modified PDF with the new slideshow timings.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}