using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFile1 = "input1.pdf";
        const string inputFile2 = "input2.pdf";
        const string outputFile = "output.pdf";

        if (!File.Exists(inputFile1))
        {
            Console.Error.WriteLine($"File not found: {inputFile1}");
            return;
        }
        if (!File.Exists(inputFile2))
        {
            Console.Error.WriteLine($"File not found: {inputFile2}");
            return;
        }

        PdfFileEditor pdfEditor = new PdfFileEditor();
        bool success = pdfEditor.MakeNUp(inputFile1, inputFile2, outputFile);
        if (success)
        {
            Console.WriteLine($"2-up PDF created: {outputFile}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create 2-up PDF.");
        }
    }
}
