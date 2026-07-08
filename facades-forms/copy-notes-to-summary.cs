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

        // Open the PDF form using the Facades API
        using (Form form = new Form(inputPdf))
        {
            // Retrieve the current value of the "Notes" field
            object notesValue = form.GetField("Notes");
            string notesText = notesValue?.ToString() ?? string.Empty;

            // Copy the retrieved text into the "Summary" field
            form.FillField("Summary", notesText);

            // Save the modified document
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form updated and saved to '{outputPdf}'.");
    }
}