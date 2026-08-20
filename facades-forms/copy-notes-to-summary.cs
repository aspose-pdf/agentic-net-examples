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

        // Load the PDF form
        using (Form form = new Form(inputPdf))
        {
            // Get the text from the "Notes" field
            string notes = form.GetField("Notes")?.ToString() ?? string.Empty;

            // Copy the text to the "Summary" field
            form.FillField("Summary", notes);

            // Save the updated PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}