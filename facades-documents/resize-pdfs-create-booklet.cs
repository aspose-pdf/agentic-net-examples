using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to process
        const string inputDir = "InputPdfs";
        // Temporary directory for resized PDFs
        const string resizedDir = "ResizedPdfs";
        // Path for the intermediate concatenated PDF
        const string mergedPath = "merged.pdf";
        // Final booklet PDF output
        const string bookletPath = "booklet.pdf";

        // Validate input directory
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the temporary resized directory exists
        Directory.CreateDirectory(resizedDir);

        // Collect all PDF files in the input directory
        string[] inputFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (inputFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to process.");
            return;
        }

        // Resize each PDF to 1024x768 and save to the temporary folder
        var resizedFiles = inputFiles.Select(inputPath =>
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string resizedPath = Path.Combine(resizedDir, $"{fileName}_resized.pdf");

            using (Document doc = new Document(inputPath))
            {
                // Resize every page to the target dimensions
                foreach (Page page in doc.Pages)
                {
                    page.PageInfo.Width = 1024;
                    page.PageInfo.Height = 768;
                }

                // Save the resized document
                doc.Save(resizedPath);
            }

            Console.WriteLine($"Resized: {inputPath} -> {resizedPath}");
            return resizedPath;
        }).ToArray();

        // Concatenate all resized PDFs into a single PDF
        PdfFileEditor editor = new PdfFileEditor();
        bool concatSuccess = editor.Concatenate(resizedFiles, mergedPath);
        if (!concatSuccess)
        {
            Console.Error.WriteLine("Failed to concatenate resized PDFs.");
            return;
        }
        Console.WriteLine($"Concatenated PDF saved to: {mergedPath}");

        // Create a booklet from the concatenated PDF
        bool bookletSuccess = editor.MakeBooklet(mergedPath, bookletPath);
        if (!bookletSuccess)
        {
            Console.Error.WriteLine("Failed to create booklet PDF.");
            return;
        }
        Console.WriteLine($"Booklet PDF created at: {bookletPath}");
    }
}