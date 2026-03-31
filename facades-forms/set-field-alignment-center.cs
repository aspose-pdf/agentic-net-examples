using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        bool alignmentSet = formEditor.SetFieldAlignment("Address", FormFieldFacade.AlignCenter);
        Console.WriteLine(alignmentSet ? "Field 'Address' alignment set to center." : "Failed to set alignment: field not found.");
    }
}