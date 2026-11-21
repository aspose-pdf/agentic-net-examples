using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Currency amount to insert
        decimal amount = 12345.67m;
        // Format with currency symbol and thousand separators (e.g., $12,345.67)
        string formattedValue = amount.ToString("C", CultureInfo.GetCultureInfo("en-US"));

        // Load PDF, modify form field, and save
        using (Document doc = new Document(inputPath))
        {
            // Replace "AmountField" with the actual name of the target form field
            if (doc.Form["AmountField"] is TextBoxField amountField)
            {
                amountField.Value = formattedValue;
            }
            else
            {
                Console.Error.WriteLine("Form field 'AmountField' not found.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Currency value populated and saved to '{outputPath}'.");
    }
}