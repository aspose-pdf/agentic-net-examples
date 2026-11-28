using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "PaymentFilled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Locate the radio‑button field and set its selected option
        if (pdfDocument.Form != null && pdfDocument.Form["PaymentMethod"] is RadioButtonOptionField radioField)
        {
            radioField.Value = "CreditCard"; // selects the "CreditCard" option
        }
        else
        {
            Console.Error.WriteLine("Radio button field 'PaymentMethod' not found.");
        }

        // Persist the changes
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
