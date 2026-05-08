using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "booklet_output.pdf"; // final booklet PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Rotate all pages to landscape orientation and keep the same page size (A4)
        using (MemoryStream rotatedStream = new MemoryStream())
        {
            using (Document doc = new Document(inputPath))               // load PDF
            {
                using (PdfPageEditor pageEditor = new PdfPageEditor())   // facade for page editing
                {
                    pageEditor.BindPdf(doc);                             // bind the document
                    pageEditor.PageSize = PageSize.A4;                    // set target page size
                    pageEditor.Rotation = 90;                            // rotate 90° to make landscape
                    pageEditor.ApplyChanges();                           // apply rotation to all pages
                    pageEditor.Save(rotatedStream);                      // save rotated PDF to memory
                }
            }

            // Reset stream position before reading
            rotatedStream.Position = 0;

            // Create booklet from the rotated PDF (PdfFileEditor does NOT implement IDisposable)
            PdfFileEditor fileEditor = new PdfFileEditor();
            using (MemoryStream bookletStream = new MemoryStream())
            {
                // MakeBooklet overload that works with streams
                fileEditor.MakeBooklet(rotatedStream, bookletStream);

                // Write the resulting booklet to the output file
                File.WriteAllBytes(outputPath, bookletStream.ToArray());
            }
        }

        Console.WriteLine($"Booklet created successfully at '{outputPath}'.");
    }
}
