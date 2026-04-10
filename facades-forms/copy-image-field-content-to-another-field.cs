using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string sourceField = "Logo";
        const string targetField = "HeaderLogo";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Copy the inner content of the image field "Logo" to a new field "HeaderLogo"
        // pageNum = -1 copies to the same page as the original field
        formEditor.CopyInnerField(sourceField, targetField, -1);

        // Save the modified PDF
        formEditor.Save();

        Console.WriteLine($"Copied field '{sourceField}' to '{targetField}' in '{outputPdf}'.");
    }
}