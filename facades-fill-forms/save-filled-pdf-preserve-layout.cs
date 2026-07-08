using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (already filled or to be filled) and the destination PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "filled_output.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Use the Form facade to work with AcroForm fields.
        // The Form constructor binds the PDF document internally.
        using (Form pdfForm = new Form(inputPdfPath))
        {
            // Example of filling a text field; replace with actual field names as needed.
            // pdfForm.FillField("FullName", "John Doe");
            // pdfForm.FillField("Date", DateTime.Today.ToShortDateString());

            // Save the PDF using the facade's Save method.
            // This preserves the original layout and all existing content.
            pdfForm.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}