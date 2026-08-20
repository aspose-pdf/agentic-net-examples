using System;
using System.IO;
using Aspose.Pdf; // Document class provides XMP metadata handling

class Program
{
    // Entry point: args[0]=input PDF, args[1]=output PDF, args[2]=XMP property name, args[3]=new value
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <input.pdf> <output.pdf> <xmpPropertyName> <newValue>");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string propName   = args[2];
        string propValue  = args[3];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Update (or add) the specified XMP property using the Metadata dictionary
            pdfDoc.Metadata[propName] = propValue;

            // Save the modified PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"XMP property '{propName}' updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
