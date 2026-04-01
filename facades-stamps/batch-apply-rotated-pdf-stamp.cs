using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string sourceDirectory = @"\\networkshare\pdfs";
        string stampPdfPath = @"\\networkshare\stamp\stamp.pdf";

        if (!Directory.Exists(sourceDirectory))
        {
            Console.Error.WriteLine("Source directory not found: " + sourceDirectory);
            return;
        }

        // Change current directory so that Save can use a simple filename
        Directory.SetCurrentDirectory(sourceDirectory);

        string[] pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf");
        foreach (string pdfFilePath in pdfFiles)
        {
            string outputFileName = Path.GetFileNameWithoutExtension(pdfFilePath) + "_stamped.pdf";

            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                // Load the target PDF
                fileStamp.BindPdf(pdfFilePath);

                // Prepare the stamp (first page of the stamp PDF)
                Stamp stamp = new Stamp();
                stamp.BindPdf(stampPdfPath, 1);
                stamp.IsBackground = true;
                stamp.Rotation = 45.0f; // rotate 45 degrees

                // Apply the stamp to all pages of the target PDF
                fileStamp.AddStamp(stamp);

                // Save using only the filename (current directory is sourceDirectory)
                fileStamp.Save(outputFileName);
                fileStamp.Close();
            }

            Console.WriteLine("Stamped: " + outputFileName);
        }
    }
}