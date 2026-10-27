using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Example currency amount to insert
        decimal amount = 1234567.89m;
        // Format with currency symbol and thousand separators (e.g., $1,234,567.89)
        string formattedAmount = amount.ToString("C", CultureInfo.GetCultureInfo("en-US"));

        // Load the PDF, modify the form field, and save
        using (Document doc = new Document(inputPath))
        {
            // Replace "AmountField" with the actual name of the target form field
            TextBoxField field = doc.Form["AmountField"] as TextBoxField;
            if (field != null)
            {
                field.Value = formattedAmount;
            }
            else
            {
                Console.Error.WriteLine("Form field 'AmountField' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Currency value saved to '{outputPath}'.");
    }
}