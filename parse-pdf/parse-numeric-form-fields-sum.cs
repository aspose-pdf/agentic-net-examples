using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the form (AcroForm) associated with the document
            Form pdfForm = doc.Form;

            // Accumulate numeric values from all NumberField instances
            decimal sum = 0m;
            int count = 0;

            // Iterate over each field in the form
            foreach (Field field in pdfForm.Fields)
            {
                // Process only NumberField types
                if (field is NumberField numberField)
                {
                    // The Value property holds the field content; convert it to string
                    string rawValue = numberField.Value?.ToString() ?? string.Empty;

                    // Try to parse the string as a decimal using invariant culture
                    if (decimal.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal numericValue))
                    {
                        sum += numericValue;
                        count++;
                        Console.WriteLine($"Field '{numberField.FullName}' value: {numericValue}");
                    }
                    else
                    {
                        Console.WriteLine($"Field '{numberField.FullName}' contains non-numeric data: '{rawValue}'");
                    }
                }
            }

            Console.WriteLine($"Processed {count} numeric fields. Total sum: {sum}");
        }
    }
}