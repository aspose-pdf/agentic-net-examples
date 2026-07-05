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

        // Path for the intermediate concatenated document
        const string concatenatedPath = "combined.pdf";

        // Final output path with page numbers added
        const string outputPath = "combined_numbered.pdf";

        // Verify that all input files exist before proceeding
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        try
        {
            // ---------- Concatenate PDFs ----------
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the array of PDFs into a single PDF file.
            // This uses the built‑in Concatenate(string[], string) method.
            bool concatResult = editor.Concatenate(inputFiles, concatenatedPath);
            if (!concatResult)
            {
                Console.Error.WriteLine("Failed to concatenate PDF files.");
                return;
            }

            // ---------- Add page numbers ----------
            // PdfFileStamp implements IDisposable, so we use a using block.
            using (PdfFileStamp stamp = new PdfFileStamp())
            {
                // Bind the previously concatenated PDF.
                stamp.BindPdf(concatenatedPath);

                // Add page numbers. The character '#' is replaced with the actual page number.
                // The numbers are placed at the bottom centre of each page by default.
                stamp.AddPageNumber("#");

                // Save the final document with page numbers.
                stamp.Save(outputPath);
            }

            Console.WriteLine($"Successfully created '{outputPath}' with concatenated content and page numbers.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the intermediate file if it exists.
            if (File.Exists(concatenatedPath))
            {
                try { File.Delete(concatenatedPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}