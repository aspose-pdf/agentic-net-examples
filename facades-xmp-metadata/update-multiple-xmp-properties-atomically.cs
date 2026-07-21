using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileInfo, PdfXmpMetadata

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF file information facade
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            // Load XMP metadata facade bound to the same PDF
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPath);

                // Add multiple XMP properties in a single transaction
                xmp.Add("xmp:CreatorTool", "MyCustomTool");
                xmp.Add("xmp:Nickname", "SampleNick");
                xmp.Add("xmp:CreateDate", DateTime.UtcNow);
                xmp.Add("xmp:ModifyDate", DateTime.UtcNow);

                // Save all changes atomically; other metadata remains untouched
                bool saved = fileInfo.SaveNewInfoWithXmp(outputPath);
                Console.WriteLine(saved
                    ? $"XMP metadata updated and saved to '{outputPath}'."
                    : "Failed to save updated XMP metadata.");
            }
        }
    }
}