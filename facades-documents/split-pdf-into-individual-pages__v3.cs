using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "SplitPages";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual page streams
            MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

            // Save each page as a separate PDF file in the output folder
            for (int i = 0; i < pageStreams.Length; i++)
            {
                string pagePath = Path.Combine(outputFolder, $"page_{i + 1}.pdf");
                using (FileStream fs = new FileStream(pagePath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].WriteTo(fs);
                }
                // Dispose the memory stream for the current page
                pageStreams[i].Dispose();
            }

            Console.WriteLine($"PDF successfully split into individual pages under '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF split: {ex.Message}");
        }
    }
}
