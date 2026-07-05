using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs to process
        string inputFolder = args.Length > 0 ? args[0] : "InputPdfs";
        // Output folder for final booklet
        string outputFolder = args.Length > 1 ? args[1] : "Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Temporary folder to store resized PDFs
        string tempFolder = Path.Combine(outputFolder, "ResizedTemp");
        Directory.CreateDirectory(tempFolder);

        // Collect all PDF files from the input folder
        string[] sourceFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (sourceFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to process.");
            return;
        }

        // Resize each PDF to 1024x768 and store in the temporary folder
        string[] resizedFiles = new string[sourceFiles.Length];
        for (int i = 0; i < sourceFiles.Length; i++)
        {
            string src = sourceFiles[i];
            string resizedPath = Path.Combine(tempFolder,
                Path.GetFileNameWithoutExtension(src) + "_resized.pdf");
            resizedFiles[i] = resizedPath;

            // PdfFileEditor.ResizeContents(string inputFile, string outputFile,
            //     int[] pages, double newWidth, double newHeight)
            // Passing null for pages applies the operation to all pages.
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(src, resizedPath, null, 1024, 768);
        }

        // Concatenate all resized PDFs into a single PDF
        string concatenatedPath = Path.Combine(outputFolder, "concatenated.pdf");
        PdfFileEditor concatEditor = new PdfFileEditor();
        concatEditor.Concatenate(resizedFiles, concatenatedPath);

        // Create a booklet from the concatenated PDF
        string bookletPath = Path.Combine(outputFolder, "booklet.pdf");
        PdfFileEditor bookletEditor = new PdfFileEditor();
        bookletEditor.MakeBooklet(concatenatedPath, bookletPath);

        // Optional: clean up temporary resized files
        try
        {
            foreach (var file in resizedFiles)
                File.Delete(file);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }

        Console.WriteLine($"Booklet created at: {bookletPath}");
    }
}