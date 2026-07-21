using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point: expects at least three arguments:
    // 1. Input PDF file path
    // 2. XMP property name (e.g., "dc:creator")
    // 3. New value for the property
    // 4. Optional output PDF file path (if omitted, a default name is generated)
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <input.pdf> <propertyName> <propertyValue> [output.pdf]");
            return;
        }

        string inputPath  = args[0];
        string propName   = args[1];
        string propValue  = args[2];
        string outputPath = args.Length >= 4 ? args[3] : GetDefaultOutputPath(inputPath);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Use PdfXmpMetadata facade to bind the PDF, modify XMP, and save.
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Load the PDF document.
                xmp.BindPdf(inputPath);

                // Add or update the specified XMP property.
                // The Add(string, object) overload accepts a plain string value.
                xmp.Add(propName, propValue);

                // Persist the changes to a new PDF file.
                xmp.Save(outputPath);
            }

            Console.WriteLine($"XMP property '{propName}' updated successfully.");
            Console.WriteLine($"Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error updating XMP metadata: {ex.Message}");
        }
    }

    // Generates a default output file name by appending "_updated" before the extension.
    private static string GetDefaultOutputPath(string inputPath)
    {
        string directory = Path.GetDirectoryName(inputPath);
        string filename  = Path.GetFileNameWithoutExtension(inputPath);
        string extension = Path.GetExtension(inputPath);
        return Path.Combine(directory, $"{filename}_updated{extension}");
    }
}