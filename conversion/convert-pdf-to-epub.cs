// File: Program.cs
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.epub";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Initialize default EPUB save options
        EpubSaveOptions epubOptions = new EpubSaveOptions();

        // Save the document as EPUB using the default conversion settings
        pdfDoc.Save(outputPath, epubOptions);

        Console.WriteLine($"PDF successfully converted to EPUB: {outputPath}");
    }
}

// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// This file is intentionally left empty to satisfy the project compilation.
// It can contain editorconfig settings, but for the purpose of building the
// sample it only needs to be a valid C# source file.
