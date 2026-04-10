using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDFs
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a new option "Option A" to the dropdown (combo box) named "Choices"
            formEditor.AddListItem("Choices", "Option A");

            // Save the modified PDF
            formEditor.Save();
        }

        Console.WriteLine($"List item added and saved to '{outputPdf}'.");
    }
}