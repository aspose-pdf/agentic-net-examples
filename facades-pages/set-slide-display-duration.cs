using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_durations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (recommended disposal pattern)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Example: set display duration for each page (in seconds)
            // Adjust the page numbers and durations as needed
            int[] pageNumbers = { 1, 2, 3, 4, 5 }; // pages to modify
            int[] durations   = { 5, 10, 7, 12, 8 }; // corresponding durations in seconds

            for (int i = 0; i < pageNumbers.Length; i++)
            {
                // Specify the page to edit (ProcessPages expects an int array)
                editor.ProcessPages = new int[] { pageNumbers[i] };

                // Set the display duration (integer seconds)
                editor.DisplayDuration = durations[i];

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF (PDF output, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated slide durations to '{outputPath}'.");
    }
}