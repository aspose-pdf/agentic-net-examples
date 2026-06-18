using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfFileEditor to split the PDF into single‑page streams
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdfPath);

        // Iterate over the returned streams and write each to a uniquely named PDF file
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Reset stream position before reading
            pageStreams[i].Position = 0;

            string outputPath = Path.Combine(outputFolder, $"page{i + 1}.pdf");

            // Write the stream content to a file
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                pageStreams[i].WriteTo(fileStream);
            }

            // Dispose the memory stream after it has been saved
            pageStreams[i].Dispose();

            Console.WriteLine($"Saved page {i + 1} to '{outputPath}'.");
        }
    }
}