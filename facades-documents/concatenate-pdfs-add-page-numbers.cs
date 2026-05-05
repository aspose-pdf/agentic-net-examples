using System;
using System.IO;
using Aspose.Pdf;
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
        const string combinedPath = "combined.pdf";

        // Final output PDF with page numbers
        const string outputPath = "final_output.pdf";

        // Verify that all input files exist
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
            // PdfFileEditor does NOT implement IDisposable, so no using block is required.
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate all input files into a single PDF.
            // This overload takes an array of file names and the destination file name.
            bool concatSuccess = editor.Concatenate(inputFiles, combinedPath);
            if (!concatSuccess)
            {
                Console.Error.WriteLine("Concatenation failed.");
                return;
            }

            // ---------- Add page numbers ----------
            // PdfFileStamp can be constructed with input and output file paths.
            // It provides AddPageNumber methods to insert page numbers.
            PdfFileStamp stamp = new PdfFileStamp(combinedPath, outputPath);

            // Add page numbers using the default format "Page #"
            // The "#" placeholder will be replaced by the actual page number.
            stamp.AddPageNumber("Page #");

            // Close the stamp to finalize and write the output file.
            stamp.Close();

            // Optionally delete the intermediate combined file.
            if (File.Exists(combinedPath))
            {
                File.Delete(combinedPath);
            }

            Console.WriteLine($"Successfully created '{outputPath}' with page numbers.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}