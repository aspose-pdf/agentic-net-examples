using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, field name to remove, output PDF path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <inputPdf> <fieldName> <outputPdf>");
            return;
        }

        string inputPdf = args[0];
        string fieldName = args[1];
        string outputPdf = args[2];

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create the FormEditor facade
            FormEditor formEditor = new FormEditor();

            // Load the PDF document
            formEditor.BindPdf(inputPdf);

            // Remove the specified form field
            formEditor.RemoveField(fieldName);

            // Save the modified PDF (uses the document-save rule)
            formEditor.Save(outputPdf);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}