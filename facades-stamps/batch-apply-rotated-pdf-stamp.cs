using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Network share containing source PDFs
        const string sourceFolder = @"\\networkshare\pdfs";
        // Folder where stamped PDFs will be written
        const string outputFolder = @"\\networkshare\pdfs\Stamped";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files in the share (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath = Path.Combine(outputFolder, fileName);

            // Load the source PDF
            using (var document = new Document(pdfPath))
            {
                // Create a TextStamp (core API) – this is the correct way to add a rotated text watermark.
                var stamp = new TextStamp("CONFIDENTIAL")
                {
                    Background = false,               // draw on top of page content
                    RotateAngle = 45,                 // rotate stamp 45 degrees
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                stamp.TextState.FontSize = 48;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Color.Red;

                // Apply the stamp to every page in the document
                foreach (Page page in document.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the stamped PDF to the output location
                document.Save(outPath);
            }

            Console.WriteLine($"Stamped PDF saved: {outPath}");
        }
    }
}
