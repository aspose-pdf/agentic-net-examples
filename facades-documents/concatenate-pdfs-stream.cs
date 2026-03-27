using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <input1.pdf> <input2.pdf> [<input3.pdf> ...]");
            return;
        }

        string outputFile = "merged.pdf";

        List<FileStream> inputFileStreams = new List<FileStream>();
        try
        {
            foreach (string inputPath in args)
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    return;
                }
                FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
                inputFileStreams.Add(fs);
            }

            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                PdfFileEditor editor = new PdfFileEditor();
                Stream[] inputArray = inputFileStreams.ToArray();
                bool success = editor.Concatenate(inputArray, outputStream);
                if (success)
                {
                    Console.WriteLine($"Successfully concatenated {args.Length} files into '{outputFile}'.");
                }
                else
                {
                    Console.Error.WriteLine("Concatenation failed.");
                }
            }
        }
        finally
        {
            foreach (FileStream fs in inputFileStreams)
            {
                fs.Dispose();
            }
        }
    }
}
