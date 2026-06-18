using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = new string[]
        {
            "file1.pdf",
            "file2.pdf",
            "file3.pdf"
        };

        // Path for the intermediate concatenated PDF
        const string concatenatedPath = "combined.pdf";

        // Path for the final PDF with page numbers
        const string finalOutputPath = "final_with_page_numbers.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // -------------------------------------------------
        // 1. Concatenate the PDFs using PdfFileEditor
        // -------------------------------------------------
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Concatenate all input files into a single PDF
        bool concatSuccess = fileEditor.Concatenate(inputFiles, concatenatedPath);
        if (!concatSuccess)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // -------------------------------------------------
        // 2. Add page numbers to the concatenated PDF
        // -------------------------------------------------
        // PdfFileStamp works with an input PDF and produces an output PDF.
        // Use the default constructor, bind the concatenated PDF, add page numbers,
        // then save to the final output path.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the concatenated PDF as the source document
            fileStamp.BindPdf(concatenatedPath);

            // Optional: set the starting page number (default is 1)
            // fileStamp.StartingNumber = 1;

            // Add page numbers. The format string may contain '#' which will be replaced
            // by the actual page number. This adds numbers at the bottom‑middle position.
            fileStamp.AddPageNumber("Page #");

            // Save the result to the final output file
            fileStamp.Save(finalOutputPath);
        }

        // Clean up the intermediate file if desired
        try
        {
            File.Delete(concatenatedPath);
        }
        catch
        {
            // Ignored – the file may be in use or deletion may fail
        }

        Console.WriteLine($"PDFs concatenated and numbered successfully: {finalOutputPath}");
    }
}