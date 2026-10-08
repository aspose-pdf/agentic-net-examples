using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newSubject = "This PDF contains the quarterly financial report";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF metadata using the Facades API
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Update the Subject field
            info.Subject = newSubject;

            // Save the modified PDF
            info.Save(outputPath);
        }

        Console.WriteLine($"Subject updated and saved to '{outputPath}'.");
    }
}