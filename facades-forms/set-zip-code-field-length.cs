using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output24.pdf";
        const string fieldName  = "ZipCode";
        const int    maxLength  = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the PDF document to the FormEditor.
            formEditor.BindPdf(inputPath);

            // Set the maximum number of characters allowed for the ZipCode field.
            formEditor.SetFieldLimit(fieldName, maxLength);

            // Save the updated PDF to the specified output file.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}