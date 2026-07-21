using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC paths to the source PDFs, the stamp PDF, and the output folder
        const string sourceFolder = @"\\NetworkShare\PdfFiles";
        const string stampPdfPath = @"\\NetworkShare\Stamp\stamp.pdf";
        const string outputFolder = @"\\NetworkShare\StampedPdfFiles";

        // Ensure the output directory exists
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        // Retrieve all PDF files from the source folder
        string[] pdfFiles;
        try
        {
            pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to enumerate PDF files in '{sourceFolder}': {ex.Message}");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_stamped.pdf");

            try
            {
                // Initialise the facade for stamping
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.InputFile = inputPath;      // source PDF
                fileStamp.OutputFile = outputPath;    // destination PDF

                // Create a stamp that uses the first page of another PDF
                Stamp stamp = new Stamp();
                stamp.BindPdf(stampPdfPath, 1);       // bind page 1 of the stamp PDF
                stamp.Rotation = 45f;                 // rotate the stamp (degrees)
                stamp.IsBackground = true;           // place stamp behind page content

                // Apply the stamp to all pages of the source document
                fileStamp.AddStamp(stamp);
                fileStamp.Close();                    // saves the output file

                Console.WriteLine($"Stamped file saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}