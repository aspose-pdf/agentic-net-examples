using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains the legal disclaimer (single‑page PDF)
        const string disclaimerPdf = "disclaimer.pdf";

        // Folder containing the target PDFs to be stamped
        const string inputFolder = "InputPdfs";

        // Folder where stamped PDFs will be saved
        const string outputFolder = "StampedPdfs";

        if (!File.Exists(disclaimerPdf))
        {
            Console.Error.WriteLine($"Disclaimer file not found: {disclaimerPdf}");
            return;
        }

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_stamped.pdf");

            // Configure PdfFileStamp with input and output files
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.InputFile = inputPath;
                fileStamp.OutputFile = outputPath;

                // Create a stamp that uses the first page of the disclaimer PDF
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindPdf(disclaimerPdf, 1);          // use page 1 of disclaimer.pdf
                stamp.IsBackground = false;               // overlay on top of existing content
                stamp.Opacity = 1.0f;                     // fully opaque
                stamp.Pages = new int[] { 1 };            // apply only to the first page

                // Add the stamp to the document and finalize
                fileStamp.AddStamp(stamp);
                fileStamp.Close(); // saves the result to OutputFile
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
        }
    }
}