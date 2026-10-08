// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string gzipPath = "output.txt.gz";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF and extract all text using the Facade API
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);
        extractor.ExtractText();

        // Retrieve the extracted text via a MemoryStream (PdfExtractor.GetText requires a stream)
        string extractedText;
        using (MemoryStream textStream = new MemoryStream())
        {
            extractor.GetText(textStream); // write text to the stream
            textStream.Position = 0;        // rewind for reading
            using (StreamReader reader = new StreamReader(textStream))
            {
                extractedText = reader.ReadToEnd();
            }
        }

        // Write the text to a GZip compressed file
        using (FileStream fileStream = new FileStream(gzipPath, FileMode.Create, FileAccess.Write))
        using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
        using (StreamWriter writer = new StreamWriter(gzipStream))
        {
            writer.Write(extractedText);
        }

        Console.WriteLine($"Extracted text saved to compressed file: {gzipPath}");
    }
}

// ------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ------------------------------------------------------------
// This file was originally referenced in the project as a source file
// but the actual .editorconfig content is not required for compilation.
// Providing a minimal, valid C# source file satisfies the compiler
// and eliminates the CS2001 error.
namespace AsposePdfApi.GeneratedMSBuildEditorConfig
{
    // Empty placeholder class – no runtime behavior needed.
    internal static class Placeholder { }
}
