using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Resize a single PDF to 1024x768 points and save it to the specified output path.
    private static void ResizePdf(string inputPath, string outputPath)
    {
        // PdfPageEditor implements IDisposable via SaveableFacade, so we can use a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Set the desired page size (width = 1024 points, height = 768 points).
            editor.PageSize = new PageSize(1024, 768);

            // Apply the changes to all pages.
            editor.ApplyChanges();

            // Save the resized PDF.
            editor.Save(outputPath);
        }
    }

    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Define the list of source PDF files to process.
        // -----------------------------------------------------------------
        string[] sourceFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
            // Add more file names as needed.
        };

        // Validate that each source file exists.
        foreach (string file in sourceFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // 2. Resize each PDF and store the intermediate results in temp files.
        // -----------------------------------------------------------------
        List<string> resizedFiles = new List<string>();
        try
        {
            foreach (string src in sourceFiles)
            {
                // Create a temporary file name with .pdf extension.
                string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

                // Perform the resize operation.
                ResizePdf(src, tempPath);

                // Keep track of the temporary file for later concatenation.
                resizedFiles.Add(tempPath);
            }

            // -----------------------------------------------------------------
            // 3. Concatenate all resized PDFs into a single document.
            // -----------------------------------------------------------------
            string outputPath = "merged_resized.pdf";

            PdfFileEditor fileEditor = new PdfFileEditor();

            // Concatenate the array of resized files.
            bool concatenated = fileEditor.Concatenate(resizedFiles.ToArray(), outputPath);

            if (concatenated)
            {
                Console.WriteLine($"Successfully created merged PDF: {outputPath}");
            }
            else
            {
                Console.Error.WriteLine("Concatenation failed. Check the input files and permissions.");
            }
        }
        finally
        {
            // -----------------------------------------------------------------
            // 4. Clean up temporary files regardless of success or failure.
            // -----------------------------------------------------------------
            foreach (string tempFile in resizedFiles)
            {
                try
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
                catch
                {
                    // Suppress any errors during cleanup.
                }
            }
        }
    }
}