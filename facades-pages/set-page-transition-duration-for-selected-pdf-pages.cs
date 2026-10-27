using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Define the page range (example: pages 2 through 5).
        int startPage = 2; // 1‑based index
        int endPage   = 5;

        // Clamp the range to the actual page count.
        int pageCount = pdfDocument.Pages.Count;
        if (endPage > pageCount) endPage = pageCount;
        if (startPage < 1) startPage = 1;
        if (startPage > endPage)
        {
            Console.Error.WriteLine("Invalid page range.");
            return;
        }

        // Build an array with the page numbers that will receive the transition.
        int rangeLength = endPage - startPage + 1;
        int[] pagesToProcess = new int[rangeLength];
        for (int i = 0; i < rangeLength; i++)
            pagesToProcess[i] = startPage + i;

        // Use PdfPageEditor (Aspose.Pdf.Facades) to set transition type and duration.
        // TransitionType is an integer that maps to the internal enum (e.g., 4 = Cover, 11 = Fade).
        // Here we use 11 (Fade) as a common transition.
        using (PdfPageEditor editor = new PdfPageEditor(pdfDocument))
        {
            editor.ProcessPages = pagesToProcess;   // pages to which the settings apply
            editor.TransitionType = 11;            // Fade transition (integer value)
            editor.TransitionDuration = 1;         // 1 second
            editor.ApplyChanges();                  // Apply the changes to the document
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Transition duration set and saved to '{outputPath}'.");
    }
}
