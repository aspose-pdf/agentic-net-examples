using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (already filled or to be filled) and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output_filled.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the Form facade with the source PDF.
        // The Form class inherits from SaveableFacade, which provides the Save method.
        Form formFacade = new Form(inputPath);

        // OPTIONAL: Fill a form field (replace with actual field name/value as needed).
        // This operation does not alter the original page layout.
        formFacade.FillField("FieldName", "FieldValue");

        // Save the modified PDF using the facade's Save method.
        // This preserves the original layout because the facade works on the existing document.
        formFacade.Save(outputPath);

        // Release resources held by the facade.
        formFacade.Close();

        Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
    }
}