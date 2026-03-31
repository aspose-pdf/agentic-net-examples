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

        // Load existing PDF file information
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        // Example modification: change the title metadata
        fileInfo.Title = "Updated Title";

        bool saved = false;
        try
        {
            saved = fileInfo.SaveNewInfo(outputPath);
        }
        catch (IOException ioEx)
        {
            Console.Error.WriteLine("IOException during SaveNewInfo: " + ioEx.Message);
            // If the output file exists and is read‑only, clear the attribute and retry
            if (File.Exists(outputPath))
            {
                FileAttributes attrs = File.GetAttributes(outputPath);
                if ((attrs & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    File.SetAttributes(outputPath, attrs & ~FileAttributes.ReadOnly);
                    Console.WriteLine("Read‑only attribute cleared. Retrying save...");
                    try
                    {
                        saved = fileInfo.SaveNewInfo(outputPath);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine("Retry failed: " + ex.Message);
                    }
                }
            }
        }

        if (saved)
        {
            Console.WriteLine("File information saved to " + outputPath);
        }
        else
        {
            Console.Error.WriteLine("Failed to save file information.");
        }
    }
}