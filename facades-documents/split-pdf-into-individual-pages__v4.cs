using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF to be split
        const string inputPdf = "input.pdf";

        // Folder where individual page PDFs will be saved
        const string outputFolder = "SplitPages";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Use PdfFileEditor.SplitToPages to obtain a MemoryStream for each page.
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

            for (int i = 0; i < pageStreams.Length; i++)
            {
                string outPath = Path.Combine(outputFolder, $"Page_{i + 1}.pdf");
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].WriteTo(fs);
                }
                // Dispose the stream after it has been written.
                pageStreams[i].Dispose();
            }

            Console.WriteLine($"PDF split into {pageStreams.Length} pages in folder: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}
