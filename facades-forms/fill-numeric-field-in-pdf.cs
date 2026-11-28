using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "QuantityFilled.pdf";
        const string fieldName  = "Quantity";
        const string fieldValue = "100";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF to the Form facade
            Form pdfForm = new Form();
            pdfForm.BindPdf(inputPath);

            // Fill the numeric field "Quantity" with the value 100
            pdfForm.FillField(fieldName, fieldValue);

            // Save the updated PDF
            pdfForm.Save(outputPath);

            Console.WriteLine($"Field '{fieldName}' filled and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}