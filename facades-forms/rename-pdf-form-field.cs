using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Rename the form field using FormEditor (Aspose.Pdf.Facades)
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF
            formEditor.BindPdf(inputPdf);

            // Rename the field from OldName to NewName
            formEditor.RenameField("OldName", "NewName");

            // Save the updated PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field renamed and saved to '{outputPdf}'.");
    }
}