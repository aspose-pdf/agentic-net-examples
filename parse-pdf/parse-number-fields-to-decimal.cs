using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            decimal total = 0m;

            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only text box fields that contain numeric values
                if (field is TextBoxField textBox)
                {
                    string rawValue = textBox.Value?.Trim();

                    if (!string.IsNullOrEmpty(rawValue) &&
                        decimal.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal number))
                    {
                        total += number; // accumulate for further calculations
                    }
                }
            }

            // Example calculation: write the sum back to a field named "Total" (if it exists)
            if (doc.Form["Total"] is TextBoxField totalField)
            {
                totalField.Value = total.ToString(CultureInfo.InvariantCulture);
            }

            // Save the modified PDF (using the standard Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Result saved to '{outputPath}'.");
    }
}