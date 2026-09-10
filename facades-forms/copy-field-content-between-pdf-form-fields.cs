using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing both fields
        const string outputPdf = "output.pdf";  // PDF with updated TargetNotes field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Form facade works with an input and an output file.
        // It loads the PDF, allows field manipulation, and saves the result.
        Form form = new Form(inputPdf, outputPdf);

        // Retrieve the current value of the source field.
        string sourceValue = form.GetField("SourceNotes");

        // If the source field exists, copy its value to the target field.
        if (sourceValue != null)
        {
            form.FillField("TargetNotes", sourceValue);
        }
        else
        {
            Console.Error.WriteLine("Source field 'SourceNotes' not found or empty.");
        }

        // Persist the changes to the output PDF.
        form.Save();

        Console.WriteLine($"Field content copied. Output saved to '{outputPdf}'.");
    }
}