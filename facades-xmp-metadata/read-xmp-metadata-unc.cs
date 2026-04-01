using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "\\\\Server\\Share\\Documents\\sample.pdf";
        const string outputPath = "metadata.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (PdfXmpMetadata xmpMetadata = new PdfXmpMetadata())
        {
            xmpMetadata.BindPdf(inputPath);
            byte[] rawData = xmpMetadata.GetXmpMetadata();
            string xmlContent = Encoding.UTF8.GetString(rawData);
            File.WriteAllText(outputPath, xmlContent);
            Console.WriteLine("XMP metadata saved to " + outputPath);
        }
    }
}