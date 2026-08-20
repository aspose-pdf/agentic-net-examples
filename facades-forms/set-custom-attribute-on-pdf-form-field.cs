using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "OrderNumber";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and bind it to a FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // NOTE: Aspose.Pdf.FormEditor.SetFieldAttribute supports only predefined flags
            // (NoExport, ReadOnly, Required). There is no API to set an arbitrary
            // custom attribute such as "data-id". The following call demonstrates setting
            // a standard flag; replace PropertyFlag.Required with the desired flag if needed.
            bool result = formEditor.SetFieldAttribute(fieldName, PropertyFlag.Required);
            Console.WriteLine($"SetFieldAttribute result: {result}");

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}