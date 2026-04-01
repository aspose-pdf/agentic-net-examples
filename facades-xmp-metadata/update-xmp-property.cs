using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: update-xmp-property <input.pdf> <output.pdf> <propertyName> <propertyValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string propertyName = args[2];
        string propertyValue = args[3];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
                {
                    // Update or add the XMP property
                    xmp[propertyName] = new XmpValue(propertyValue);
                    // Save the PDF with the updated XMP metadata
                    xmp.Save(outputPath);
                }
            }

            Console.WriteLine("XMP property '{0}' updated and saved to '{1}'.", propertyName, outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}
