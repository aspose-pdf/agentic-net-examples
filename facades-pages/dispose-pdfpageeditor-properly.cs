using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF, rotate the first page, and ensure resources are released.
        using (Document pdf = new Document(inputPath))
        {
            // Rotate the first page 90 degrees clockwise.
            // The Rotation enum values are on0, on90, on180, on270.
            pdf.Pages[1].Rotate = Rotation.on90;

            // Save the edited PDF to a new file.
            pdf.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
