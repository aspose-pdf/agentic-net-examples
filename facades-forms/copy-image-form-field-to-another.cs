using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string sourceField = "Logo";
        const string targetField = "HeaderLogo";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor copies fields within the same document (or to another document).
        // The constructor takes the source PDF and the destination PDF.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // CopyInnerField copies the existing field to a new field with the same position.
            // pageNum = -1 keeps the field on the original page.
            // newFieldName is the fully qualified name of the new field.
            formEditor.CopyInnerField(sourceField, targetField, -1);
            formEditor.Save();
        }

        Console.WriteLine($"Field '{sourceField}' copied to '{targetField}' in '{outputPdf}'.");
    }
}