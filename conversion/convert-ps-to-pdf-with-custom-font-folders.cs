using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPs = "input.ps";
        const string outputPdf = "output.pdf";

        // Define custom font directories (adjust paths as needed)
        string[] customFontFolders = new string[]
        {
            @"C:\MyFonts",
            @"D:\OtherFonts"
        };

        if (!File.Exists(inputPs))
        {
            Console.Error.WriteLine($"Input file not found: {inputPs}");
            return;
        }

        // Filter only the directories that actually exist
        string[] existingFolders = customFontFolders
                                    .Where(Directory.Exists)
                                    .ToArray();

        // Set up PS load options with the custom font folders
        PsLoadOptions loadOptions = new PsLoadOptions
        {
            FontsFolders = existingFolders
        };

        // Load the PostScript file and convert it to PDF
        using (Document doc = new Document(inputPs, loadOptions))
        {
            // Save the document as PDF (default format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PostScript file converted and saved as PDF: {outputPdf}");
    }
}