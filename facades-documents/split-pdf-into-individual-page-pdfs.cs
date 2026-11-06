using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SplitPages_Output";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // 1. Use PdfFileEditor.SplitToPages to split the PDF into individual
        //    page streams (MemoryStream[]). This method returns an array where
        //    each element contains the bytes of a single page.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdfPath);

        // -----------------------------------------------------------------
        // 2. Write each MemoryStream to a separate PDF file in the output folder.
        // -----------------------------------------------------------------
        for (int i = 0; i < pageStreams.Length; i++)
        {
            MemoryStream ms = pageStreams[i];
            ms.Position = 0; // reset stream position before reading

            string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.pdf");
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(outStream);
            }

            // Dispose the stream now that we're done with it
            ms.Dispose();

            Console.WriteLine($"Page saved: {outputPath}");
        }

        Console.WriteLine("PDF split into individual pages completed.");
    }
}
