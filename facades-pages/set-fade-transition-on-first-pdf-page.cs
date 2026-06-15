using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfPageEditor (a Facades class) to edit page transitions
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(inputPdf);

            // Specify that only the first page will be edited
            editor.ProcessPages = new int[] { 1 };

            // Set transition type to Fade.
            // In Aspose.Pdf the integer value 0 corresponds to the Fade transition.
            editor.TransitionType = 0;

            // Set transition duration to 2 seconds
            editor.TransitionDuration = 2;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with Fade transition on page 1: {outputPdf}");
    }
}