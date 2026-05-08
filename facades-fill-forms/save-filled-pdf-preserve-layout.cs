using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source (filled) PDF and the destination file
        const string inputPath  = "filled_input.pdf";
        const string outputPath = "preserved_output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Use the Form facade (inherits SaveableFacade) to bind the PDF.
        // This facade works directly with the PDF document and preserves its layout.
        Form formFacade = new Form();
        formFacade.BindPdf(inputPath);

        // OPTIONAL: fill form fields if needed.
        // formFacade.FillField("Form1.TextBox1", "Example value");

        // Save the PDF to the specified output path, keeping the original layout intact.
        formFacade.Save(outputPath);

        // Release any resources held by the facade.
        formFacade.Close();

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}