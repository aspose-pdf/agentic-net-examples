using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input PDF path
        // 1 - output PDF path
        // 2 - XMP property name (e.g., "dc:creator")
        // 3 - XMP property value
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <input.pdf> <output.pdf> <xmpPropertyName> <xmpPropertyValue>");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string propName   = args[2];
        string propValue  = args[3];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF and manipulate its XMP metadata
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Load the source PDF
                xmp.BindPdf(inputPath);

                // If the property already exists, remove it first
                if (xmp.Contains(propName))
                {
                    xmp.Remove(propName);
                }

                // Add or update the XMP property
                // The Add(string, object) overload creates the appropriate XmpValue internally
                xmp.Add(propName, propValue);

                // Save the updated PDF with the new XMP metadata
                xmp.Save(outputPath);
            }

            Console.WriteLine($"XMP property '{propName}' updated successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}