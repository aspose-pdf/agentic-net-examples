using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Custom left and right page sequences for the booklet
        int[] leftPages = new int[] { 2, 4, 6 };
        int[] rightPages = new int[] { 1, 3, 5, 7 };

        // Create the PdfFileEditor and generate the customized booklet
        PdfFileEditor editor = new PdfFileEditor();
        bool created = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);
        if (!created)
        {
            Console.Error.WriteLine("MakeBooklet operation failed.");
            return;
        }

        // Load the resulting booklet to inspect page order
        using (Document booklet = new Document(outputPath))
        {
            Console.WriteLine($"Booklet created successfully. Total pages: {booklet.Pages.Count}");

            // Extract and display text from each page to verify ordering
            for (int i = 1; i <= booklet.Pages.Count; i++)
            {
                // Create a fresh TextAbsorber for each page (TextAbsorber.Text is read‑only)
                TextAbsorber absorber = new TextAbsorber();
                booklet.Pages[i].Accept(absorber);
                Console.WriteLine($"Page {i}: {absorber.Text.Trim()}");
            }
        }
    }
}
