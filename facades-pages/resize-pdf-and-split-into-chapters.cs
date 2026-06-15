using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Chapters";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Resize all pages to a uniform size (A4) and apply a consistent zoom factor.
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Load the source PDF.
            pageEditor.BindPdf(inputPdf);

            // Set a uniform page size. PageSize.A4 is a predefined size (595 x 842 points).
            pageEditor.PageSize = PageSize.A4;

            // Set a uniform zoom (1.0 = 100%). Adjust if you need scaling.
            pageEditor.Zoom = 1.0f;

            // Save the resized document to a memory stream.
            using (MemoryStream resizedStream = new MemoryStream())
            {
                pageEditor.Save(resizedStream);
                resizedStream.Position = 0; // Reset stream for reading.

                // Split the resized PDF into individual pages (each treated as a chapter).
                PdfFileEditor fileEditor = new PdfFileEditor();
                MemoryStream[] pageStreams = fileEditor.SplitToPages(resizedStream);

                for (int i = 0; i < pageStreams.Length; i++)
                {
                    string outPath = Path.Combine(outputDir, $"Chapter_{i + 1}.pdf");
                    using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        pageStreams[i].WriteTo(outFile);
                    }
                    pageStreams[i].Dispose();
                }
            }
        }

        Console.WriteLine("PDF has been resized and split into separate chapter files.");
    }
}