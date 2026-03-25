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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Aspose.Pdf.Document does not provide a Split method.
            // Use PdfFileEditor.SplitToPages which returns a MemoryStream per page.
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(inputPath);

            for (int i = 0; i < pageStreams.Length; i++)
            {
                string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].WriteTo(fs);
                }
                Console.WriteLine($"Saved page {i + 1} → {outPath}");
                pageStreams[i].Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
