using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string nickname = "CustomIdentifier123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Set the Nickname in XMP metadata using the Metadata dictionary
        // The key follows the XMP namespace prefix convention.
        pdfDoc.Metadata["xmp:Nickname"] = nickname;

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Nickname set to '{nickname}' and saved to '{outputPath}'.");
    }
}
