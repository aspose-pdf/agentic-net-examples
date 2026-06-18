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

        // Initialize the PdfPageEditor facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve total number of pages (1‑based indexing)
            int pageCount = editor.GetPages();

            // Set a display duration (in seconds) for each page
            // Here each page is set to 5 seconds; adjust as needed
            for (int i = 1; i <= pageCount; i++)
            {
                // Specify the page to edit
                editor.ProcessPages = new int[] { i };

                // Assign the desired duration
                editor.DisplayDuration = 5;

                // Apply the change to the current page
                editor.ApplyChanges();
            }

            // Save the modified PDF with the new timing information
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}