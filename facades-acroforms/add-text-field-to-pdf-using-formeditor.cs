using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the form
        const string inputPdf = "input.pdf";
        // Output PDF with the new text field
        const string outputPdf = "output.pdf";

        // Field specifications
        const string fieldName = "NewTextField";
        const int pageNumber = 1;               // 1‑based page index
        const float llx = 100f;                 // lower‑left X
        const float lly = 200f;                 // lower‑left Y
        const float urx = 300f;                 // upper‑right X
        const float ury = 220f;                 // upper‑right Y

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialise FormEditor with source and destination files
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Add a text field to the specified page and coordinates
        bool success = formEditor.AddField(FieldType.Text, fieldName, pageNumber, llx, lly, urx, ury);
        if (!success)
        {
            Console.Error.WriteLine("Failed to add the text field.");
            return;
        }

        // Save the modified PDF
        formEditor.Save();

        Console.WriteLine($"Text field '{fieldName}' added to page {pageNumber} and saved as '{outputPdf}'.");
    }
}