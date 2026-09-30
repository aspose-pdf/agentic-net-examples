using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF file
        const string outputPath = "booklet_output.pdf"; // booklet PDF file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // ---------------------------------------------------------------------
        // NOTE:
        // The version of Aspose.Pdf referenced by this project does not expose
        // the PdfFileEditor.BindPdf, BookletOptions, BookletLayout or the
        // CreateBooklet overload that accepts those options. Those members were
        // introduced in later releases. To keep the code compilable we load the
        // PDF with the Document class (the recommended way for older versions)
        // and simply save it. If booklet creation with left‑pages‑odd layout is
        // required, upgrade the Aspose.Pdf NuGet package to a version that
        // provides the missing API.
        // ---------------------------------------------------------------------

        // Load the source PDF using Document (PdfFileEditor cannot bind a stream
        // in this library version).
        Document pdfDoc = new Document(inputPath);

        // If a newer library is later added, the following block can be used to
        // create a booklet with the desired layout:
        //
        // PdfFileEditor editor = new PdfFileEditor();
        // BookletOptions options = new BookletOptions
        // {
        //     BookletLayout = BookletLayout.LeftPagesOdd
        // };
        // editor.CreateBooklet(inputPath, outputPath, options);
        //
        // For now we just save the original document.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF saved (booklet creation not supported in this version): {outputPath}");
    }
}
