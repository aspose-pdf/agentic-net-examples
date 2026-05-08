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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Form facade works with AcroForm/XFA fields.
        // Constructor takes source and destination file names.
        using (Form form = new Form(inputPdf, outputPdf))
        {
            // Rename the field throughout the document.
            form.RenameField("OldName", "NewName");

            // Persist changes.
            form.Save();
        }

        Console.WriteLine($"Field renamed and saved to '{outputPdf}'.");
    }
}