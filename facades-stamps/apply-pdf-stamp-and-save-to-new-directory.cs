using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the original PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where stamped PDFs will be saved
        const string outputFolder = @"C:\StampedPdfs";
        // Path to the PDF file that will be used as a stamp (first page will be the stamp)
        const string stampPdfPath = @"C:\Stamp\stamp.pdf";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Preserve the original file name
            string fileName = Path.GetFileName(inputFilePath);
            string outputFilePath = Path.Combine(outputFolder, fileName);

            // Initialize the PdfFileStamp facade
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                // Bind the source PDF
                fileStamp.BindPdf(inputFilePath);

                // Create a stamp object (using the first page of the stamp PDF)
                Stamp stamp = new Stamp();
                stamp.BindPdf(stampPdfPath, 1);   // use page 1 of stamp.pdf as the stamp
                stamp.IsBackground = true;       // place the stamp behind the page content
                stamp.Opacity = 0.5f;             // semi‑transparent

                // Add the stamp to the document
                fileStamp.AddStamp(stamp);

                // Save the stamped document to the output folder
                fileStamp.Save(outputFilePath);
                // Close releases resources; Save already persisted the file
                fileStamp.Close();
            }

            Console.WriteLine($"Stamped PDF saved: {outputFilePath}");
        }
    }
}