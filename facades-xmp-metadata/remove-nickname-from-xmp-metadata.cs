using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required per task specification

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove the Nickname element from XMP metadata if it exists.
            // In Aspose.Pdf the XMP metadata is exposed via the Document.Metadata dictionary.
            const string nicknameKey = "xmp:Nickname";
            if (doc.Metadata != null && doc.Metadata.ContainsKey(nicknameKey))
            {
                doc.Metadata.Remove(nicknameKey);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Nickname element removed. Saved to '{outputPath}'.");
    }
}
