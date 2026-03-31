using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        PdfFileEditor fileEditor = new PdfFileEditor();
        // Shrink contents to 60% of original width and height, creating a 20% margin on each side.
        bool success = fileEditor.ResizeContentsPct(inputPath, outputPath, null, 60.0, 60.0);

        if (success)
        {
            Console.WriteLine("Resizing completed. Output saved to '" + outputPath + "'.");
        }
        else
        {
            Console.Error.WriteLine("Resizing failed.");
        }
    }
}