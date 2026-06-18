using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
            // add more file paths as needed
        };

        // Directory where processed PDFs will be saved
        string outputDirectory = "ProcessedPdfs";
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF in parallel
        Parallel.ForEach(inputFiles, inputFile =>
        {
            // Validate input file existence
            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine($"File not found: {inputFile}");
                return;
            }

            // Determine output file path (same name, placed in output directory)
            string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputFile));

            // Use PdfPageEditor (a Facade) to perform batch editing.
            // The facade implements IDisposable, so wrap it in a using block.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF file to the editor.
                editor.BindPdf(inputFile);

                // Example operation: rotate all pages by 90 degrees.
                // Rotation property applies to all pages unless ProcessPages is set.
                editor.Rotation = 90;

                // Apply the changes to the document.
                editor.ApplyChanges();

                // Save the edited PDF to the output path.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {inputFile} -> {outputPath}");
        });

        Console.WriteLine("All PDFs have been processed.");
    }
}