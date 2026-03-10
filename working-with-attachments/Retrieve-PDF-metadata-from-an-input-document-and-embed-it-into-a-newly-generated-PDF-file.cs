using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF and read its metadata
        using (Document sourceDoc = new Document(inputPath))
        {
            DocumentInfo srcInfo = sourceDoc.Info;

            // Create a new empty PDF document
            using (Document newDoc = new Document())
            {
                // Copy metadata from the source document
                newDoc.Info.Title            = srcInfo.Title;
                newDoc.Info.Author           = srcInfo.Author;
                newDoc.Info.Subject          = srcInfo.Subject;
                newDoc.Info.Keywords         = srcInfo.Keywords;
                newDoc.Info.Creator          = srcInfo.Creator;
                newDoc.Info.Producer         = srcInfo.Producer;
                newDoc.Info.CreationDate     = srcInfo.CreationDate;
                newDoc.Info.ModDate          = srcInfo.ModDate;
                newDoc.Info.Trapped          = srcInfo.Trapped;
                newDoc.Info.CreationTimeZone = srcInfo.CreationTimeZone;
                newDoc.Info.ModTimeZone      = srcInfo.ModTimeZone;

                // Save the new PDF with the copied metadata
                newDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Metadata copied to new PDF: {outputPath}");
    }
}