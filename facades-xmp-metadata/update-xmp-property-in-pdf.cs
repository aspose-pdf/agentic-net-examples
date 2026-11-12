using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <input.pdf> <output.pdf> <propertyName> <propertyValue>
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <input.pdf> <output.pdf> <propertyName> <propertyValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string propertyName = args[2];
        string propertyValue = args[3];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdf = new Document(inputPath);

            // Aspose.Pdf exposes XMP metadata through the Metadata dictionary.
            // Add or update the requested property.
            pdf.Metadata[propertyName] = propertyValue;

            // Save the updated PDF.
            pdf.Save(outputPath);

            Console.WriteLine($"XMP property '{propertyName}' updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
