using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade APIs for stamping

class Program
{
    static void Main()
    {
        // Folder containing the original PDFs
        const string sourceFolder = @"C:\InputPdfs";
        // Folder where stamped PDFs will be saved
        const string targetFolder = @"C:\StampedPdfs";
        // Path to the image that will be used as a stamp
        const string stampImagePath = @"C:\Stamp\stamp.png";

        // Ensure the target directory exists
        Directory.CreateDirectory(targetFolder);

        // Process each PDF file in the source folder
        foreach (string inputPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            // Preserve the original file name
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(targetFolder, fileName);

            // Create the PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();

            // Bind the source PDF (no output file property – we will save explicitly)
            fileStamp.BindPdf(inputPath);

            // Configure the stamp (image based)
            Stamp stamp = new Stamp();
            stamp.BindImage(stampImagePath);          // Use the image as stamp content
            stamp.IsBackground = true;                // Place stamp behind page content
            stamp.Opacity = 0.5f;                     // Semi‑transparent
            stamp.SetOrigin(100, 100);                // Position of the stamp on each page
            stamp.SetImageSize(150, 150);             // Size of the stamp image

            // Add the stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF to the target location
            fileStamp.Save(outputPath);

            // Close the facade (releases resources)
            fileStamp.Close();
        }

        Console.WriteLine("All PDFs have been stamped and saved.");
    }
}