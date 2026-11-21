using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files to concatenate
        string[] inputFiles = { "part1.pdf", "part2.pdf", "part3.pdf" };
        // Path for the intermediate merged PDF
        string mergedPath = "merged.pdf";
        // Final output PDF with page numbers
        string outputPath = "merged_with_page_numbers.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Concatenate PDFs using Aspose.Pdf.Facades.PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        // Concatenate overload that accepts an array of source files
        editor.Concatenate(inputFiles, mergedPath);

        // Open the merged document to add page numbers
        using (Document doc = new Document(mergedPath))
        {
            // Loop through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment with the page number
                TextFragment pageNumber = new TextFragment(i.ToString());

                // Set visual appearance
                pageNumber.TextState.Font = FontRepository.FindFont("Arial");
                pageNumber.TextState.FontSize = 12;
                pageNumber.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the page number at bottom‑right corner
                // Adjust X/Y as needed (here 500, 20 for typical A4)
                pageNumber.Position = new Position(500, 20);

                // Add the fragment to the page's paragraphs collection
                page.Paragraphs.Add(pageNumber);
            }

            // Save the final PDF (overwrites if same path)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDFs concatenated and page numbers added: {outputPath}");
    }
}