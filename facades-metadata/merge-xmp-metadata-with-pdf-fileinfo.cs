using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load and modify standard PDF file info entries
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            fileInfo.Title  = "Merged XMP and FileInfo";
            fileInfo.Author = "John Doe";

            // Load XMP metadata, add a custom entry, and merge it back
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPath);

                // Add a custom XMP property
                xmp.Add("xmp:CustomProperty", new XmpValue("CustomValue"));

                // Save the combined FileInfo and XMP metadata to a new file
                bool saved = fileInfo.SaveNewInfoWithXmp(outputPath);
                Console.WriteLine(saved
                    ? $"Metadata merged and saved to '{outputPath}'."
                    : "Failed to save merged metadata.");
            }
        }
    }
}