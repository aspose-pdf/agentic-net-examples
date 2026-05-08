using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Source directory containing original PDFs
        const string inputDir = "InputPdfs";
        // Destination directory for stamped PDFs (preserves original file names)
        const string outputDir = "StampedPdfs";

        // Verify source directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure destination directory exists
        Directory.CreateDirectory(outputDir);

        // Prepare a reusable stamp (image based)
        // Adjust origin, size, opacity as needed
        Stamp stamp = new Stamp();
        stamp.SetOrigin(100, 500);          // position of the stamp on each page
        stamp.SetImageSize(200, 100);       // width and height of the stamp
        stamp.Opacity = 0.5f;               // semi‑transparent
        stamp.IsBackground = false;        // placed over page content
        stamp.BindImage("stamp.png");       // path to the stamp image (must exist)

        // Process each PDF file in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            // Preserve original file name
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDir, fileName);

            // Initialize PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.InputFile = inputPath;   // source PDF
            fileStamp.OutputFile = outputPath; // destination PDF

            // Apply the stamp to all pages
            fileStamp.AddStamp(stamp);

            // Close saves the result and releases resources
            fileStamp.Close();
        }

        Console.WriteLine("All PDFs have been stamped and saved.");
    }
}