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

        // Use FormEditor (a SaveableFacade) to manipulate form fields.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF.
            formEditor.BindPdf(inputPdf);

            // Move the field named "DateField" to the desired rectangle on page 2.
            // Lower‑left corner (llx, lly) = (100, 200)
            // Upper‑right corner (urx, ury) = (llx + width, lly + height) = (250, 220)
            bool moved = formEditor.MoveField("DateField", 100f, 200f, 250f, 220f);

            if (!moved)
            {
                Console.Error.WriteLine("Failed to move the field 'DateField'.");
            }

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field moved and saved to '{outputPdf}'.");
    }
}