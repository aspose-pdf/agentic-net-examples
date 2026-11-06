using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Facade for bulk PDF operations
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual page streams (one MemoryStream per page)
            MemoryStream[] pageStreams = editor.SplitToPages(inputPath);

            // Write each MemoryStream to a uniquely named PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                MemoryStream ms = pageStreams[i];
                ms.Position = 0; // Ensure we start from the beginning

                string outPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");
                using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }

                ms.Dispose(); // Release the stream resources
                Console.WriteLine($"Saved page {i + 1} → '{outPath}'");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
