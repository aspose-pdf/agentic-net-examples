using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the form
        const string outputPdf = "output.pdf";  // PDF after making the field multiline
        const string fieldName = "Comments";    // name of the text field to modify

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor constructor loads the source PDF and prepares the destination file.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Convert the single‑line text field to a multiline one.
            bool success = formEditor.Single2Multiple(fieldName);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set multiline for field '{fieldName}'.");
            }

            // Save the changes to the destination PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Multiline property set for field '{fieldName}'. Output saved to '{outputPdf}'.");
    }
}