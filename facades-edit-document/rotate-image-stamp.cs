using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_stamp.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize the file stamp facade with input and output files
            PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

            // Create a stamp, bind the image, and rotate it 90 degrees clockwise
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imagePath);
            stamp.Rotation = 90f; // clockwise rotation

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);
            fileStamp.Close();

            Console.WriteLine($"Stamp added and rotated. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}