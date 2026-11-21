using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "filled_document.pdf";
        const string outputFolder = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfFileEditor does NOT implement IDisposable; no using block needed
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual page streams
            MemoryStream[] pageStreams = editor.SplitToPages(inputPdfPath);

            // Write each page stream to its own PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                string pagePath = Path.Combine(outputFolder, $"page_{i + 1}.pdf");
                using (FileStream fs = new FileStream(pagePath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].WriteTo(fs);
                }
                // Dispose the MemoryStream for the current page
                pageStreams[i].Dispose();
            }

            Console.WriteLine($"Pages exported to '{outputFolder}' as individual PDFs.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}
