using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains the legal disclaimer (first page will be used as stamp)
        const string disclaimerPath = "disclaimer.pdf";

        if (!File.Exists(disclaimerPath))
        {
            Console.Error.WriteLine($"Disclaimer file not found: {disclaimerPath}");
            return;
        }

        // List of target PDF files to which the disclaimer will be applied
        string[] targetFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        foreach (string targetPath in targetFiles)
        {
            if (!File.Exists(targetPath))
            {
                Console.Error.WriteLine($"Target file not found: {targetPath}");
                continue;
            }

            // Define output file name (original name with suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(targetPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(targetPath) + "_disclaimed.pdf");

            // Initialize PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();
            try
            {
                // Specify input and output files
                fileStamp.InputFile = targetPath;
                fileStamp.OutputFile = outputPath;

                // Create a stamp that uses the first page of the disclaimer PDF
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindPdf(disclaimerPath, 1);   // bind page 1 of disclaimer.pdf
                stamp.IsBackground = true;          // place stamp behind existing content
                stamp.Pages = new int[] { 1 };       // apply only to the first page of the target PDF

                // Add the stamp to the document and save
                fileStamp.AddStamp(stamp);
                fileStamp.Close(); // saves the result to outputPath
                Console.WriteLine($"Created: {outputPath}");
            }
            finally
            {
                // PdfFileStamp does not implement IDisposable; no further cleanup required
            }
        }
    }
}