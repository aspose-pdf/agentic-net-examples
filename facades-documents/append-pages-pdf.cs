using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPath = "dest.pdf";
        const string sourcePath = "src.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            // Append pages 1 to 2 from source PDF to the end of destination PDF.
            // Adjust startPage and endPage as needed to include the desired range.
            bool result = fileEditor.Append(destinationPath, sourcePath, 1, 2, outputPath);
            if (result)
            {
                Console.WriteLine($"Pages appended successfully. Output saved as '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Append operation failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
