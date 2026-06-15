using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one output path and one input PDF
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <output.pdf> <input1.pdf> [input2.pdf] ...");
            return;
        }

        string outputPath = args[0];

        // Collect input file paths (all arguments after the first)
        string[] inputFiles = new string[args.Length - 1];
        Array.Copy(args, 1, inputFiles, 0, args.Length - 1);

        // Verify that each input file exists
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create a temporary blank PDF (single blank page)
        string blankPagePath = Path.Combine(Path.GetTempPath(), "AsposeBlankPage.pdf");
        CreateBlankPdf(blankPagePath);

        // Build a list that interleaves a blank page between each document
        List<string> filesToConcat = new List<string>();
        for (int i = 0; i < inputFiles.Length; i++)
        {
            filesToConcat.Add(inputFiles[i]);

            // Add a blank page after each document except the last one
            if (i < inputFiles.Length - 1)
                filesToConcat.Add(blankPagePath);
        }

        // Perform concatenation using PdfFileEditor (no using block – it is not IDisposable)
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(filesToConcat.ToArray(), outputPath);

        if (success)
            Console.WriteLine($"Concatenated PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Concatenation failed.");

        // Clean up the temporary blank PDF
        try { File.Delete(blankPagePath); } catch { }
    }

    // Creates a PDF file that contains a single blank page
    static void CreateBlankPdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a blank page (default size is A4)
            doc.Pages.Add();
            doc.Save(path);
        }
    }
}