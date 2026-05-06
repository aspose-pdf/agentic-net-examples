using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "form_filled.pdf";   // PDF that already has user‑filled data
        const string outputPdf = "form_updated.pdf"; // PDF after copying Notes → Summary

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the existing PDF form
        using (Form form = new Form(inputPdf))
        {
            // Retrieve the current value of the "Notes" field
            object notesObj = form.GetField("Notes");
            string notesText = notesObj?.ToString() ?? string.Empty;

            // Write the same text into the "Summary" field
            form.FillField("Summary", notesText);

            // Save the modified document
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form updated and saved to '{outputPdf}'.");
    }
}