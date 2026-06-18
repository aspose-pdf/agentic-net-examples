using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileStamp and Stamp are in this namespace

class Program
{
    static void Main()
    {
        // Input directory containing the original PDFs
        const string inputDirectory = "InputPdfs";
        // Output directory where stamped PDFs will be saved
        const string outputDirectory = "StampedPdfs";
        // Path to the image that will be used as a stamp
        const string stampImagePath = "stamp.png";

        // Validate input directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string sourceFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Preserve the original file name for the output file
            string outputFilePath = Path.Combine(outputDirectory, Path.GetFileName(sourceFilePath));

            // Initialize PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.InputFile = sourceFilePath;   // specify source PDF
            fileStamp.OutputFile = outputFilePath; // specify destination PDF

            // Create a stamp object (image based)
            Stamp stamp = new Stamp();
            stamp.BindImage(stampImagePath);       // bind the image to be used as stamp
            stamp.IsBackground = true;             // place stamp behind page content
            stamp.Opacity = 0.5f;                  // semi‑transparent
            stamp.SetOrigin(100, 100);             // position of the stamp (lower‑left corner)
            stamp.SetImageSize(200, 100);          // size of the stamp

            // Add the stamp to the PDF and finalize
            fileStamp.AddStamp(stamp);
            fileStamp.Close(); // saves the stamped PDF to the output path
        }

        Console.WriteLine("All PDFs have been stamped and saved.");
    }
}