using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the original PDFs
        const string sourceFolder = @"C:\InputPdfs";
        // Folder where stamped PDFs will be saved
        const string destinationFolder = @"C:\StampedPdfs";
        // Path to the PDF file that will be used as a stamp (first page will be the stamp)
        const string stampPdfPath = @"C:\Stamp\stamp.pdf";

        // Ensure the destination directory exists
        Directory.CreateDirectory(destinationFolder);

        // Process each PDF file in the source folder
        foreach (string inputFilePath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            // Preserve the original file name
            string fileName = Path.GetFileName(inputFilePath);
            string outputFilePath = Path.Combine(destinationFolder, fileName);

            // Create the PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();
            // Specify input and output files via properties (as required by the rule)
            fileStamp.InputFile = inputFilePath;
            fileStamp.OutputFile = outputFilePath;

            // Create a stamp object and bind the first page of the stamp PDF
            Stamp stamp = new Stamp();
            stamp.BindPdf(stampPdfPath, 1);          // Use page 1 of the stamp PDF
            stamp.IsBackground = true;               // Place stamp behind page content
            stamp.Opacity = 0.5f;                    // Semi‑transparent

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);

            // Close the facade – this writes the stamped PDF to the output path
            fileStamp.Close();
        }

        Console.WriteLine("All PDFs have been stamped and saved to the destination folder.");
    }
}