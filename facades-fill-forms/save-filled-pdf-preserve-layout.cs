using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (already filled) and the destination PDF
        const string inputPdfPath  = "filled_input.pdf";
        const string outputPdfPath = "preserved_output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Use the Form facade to load the PDF and save it.
        // The Form facade preserves the original layout when saving.
        using (Form pdfForm = new Form(inputPdfPath))
        {
            // If additional field modifications are needed, they can be done here.
            // Example (optional):
            // pdfForm.FillField("CustomerName", "Acme Corp");

            // Save the PDF to the specified output path.
            pdfForm.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved successfully to '{outputPdfPath}'.");
    }
}