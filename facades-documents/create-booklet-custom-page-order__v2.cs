using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the generated booklet PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        // Custom page ordering for the booklet:
        // leftPages – pages that will appear on the left side of each sheet
        // rightPages – pages that will appear on the right side of each sheet
        int[] leftPages  = new int[] { 2, 4, 6 };
        int[] rightPages = new int[] { 1, 3, 5, 7 };

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the booklet using the custom page arrays
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);
        if (!success)
        {
            Console.Error.WriteLine("MakeBooklet operation failed.");
            return;
        }

        // Load the resulting booklet to verify the page order
        using (Document booklet = new Document(outputPath))
        {
            Console.WriteLine($"Booklet created successfully. Total pages: {booklet.Pages.Count}");

            // Extract text from each page to inspect the ordering.
            // This assumes the source PDF contains identifiable page numbers as text.
            for (int i = 1; i <= booklet.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                booklet.Pages[i].Accept(absorber);
                string pageText = absorber.Text?.Trim() ?? "(no text)";
                Console.WriteLine($"Page {i}: {pageText}");
            }
        }
    }
}