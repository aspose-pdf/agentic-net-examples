using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_nickname.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPath))
        {
            const string nicknameKey = "xmp:Nickname"; // XMP property name for Nickname

            // Remove the Nickname entry from the XMP metadata dictionary if it exists
            if (pdfDocument.Metadata.ContainsKey(nicknameKey))
            {
                pdfDocument.Metadata.Remove(nicknameKey);
            }

            // Save the updated PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Nickname removed. Output saved to '{outputPath}'.");
    }
}
