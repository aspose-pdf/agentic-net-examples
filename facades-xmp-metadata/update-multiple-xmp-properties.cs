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
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
            xmpMetadata.BindPdf(document);

            // Add several XMP properties in one transaction
            xmpMetadata.Add(DefaultMetadataProperties.CreatorTool, new XmpValue("Aspose.Pdf Sample"));
            xmpMetadata.Add(DefaultMetadataProperties.ModifyDate, new XmpValue(DateTime.UtcNow.ToString("o")));
            xmpMetadata.Add(DefaultMetadataProperties.Nickname, new XmpValue("SampleDoc"));

            // Save all changes atomically
            xmpMetadata.Save(outputPath);
        }

        Console.WriteLine("XMP metadata updated and saved to '" + outputPath + "'.");
    }
}