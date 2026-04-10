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

        // Open the PDF form using the Facades Form class
        using (Form form = new Form(inputPdf))
        {
            // Get the current value of the "Notes" field
            string notesValue = form.GetField("Notes")?.ToString() ?? string.Empty;

            // Copy that value into the "Summary" field
            form.FillField("Summary", notesValue);

            // Save the modified PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form updated and saved to '{outputPdf}'.");
    }
}