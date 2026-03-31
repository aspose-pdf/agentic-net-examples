using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Quantity";
        const int fieldLimit = 3; // allows up to three digits (values 1‑100)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        bool success = formEditor.SetFieldLimit(fieldName, fieldLimit);
        Console.WriteLine(success ? "Field limit set successfully." : "Failed to set field limit.");
    }
}