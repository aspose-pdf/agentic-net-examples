using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Bind the XMP metadata facade to the source PDF
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Add multiple XMP properties in a single transaction
            // Using the string/object overload for simplicity
            xmp.Add("xmp:CreatorTool", "MyApplication v1.0");
            xmp.Add("xmp:CreateDate", DateTime.UtcNow.ToString("o"));
            xmp.Add("xmp:ModifyDate", DateTime.UtcNow.ToString("o"));
            xmp.Add("xmp:Nickname", "SampleDocument");

            // Save the updated PDF; all changes are written atomically
            xmp.Save(outputPath);
        }

        Console.WriteLine($"XMP metadata updated and saved to '{outputPath}'.");
    }
}