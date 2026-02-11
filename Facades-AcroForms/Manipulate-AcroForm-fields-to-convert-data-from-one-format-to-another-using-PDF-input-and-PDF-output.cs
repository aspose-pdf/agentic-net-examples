using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the AcroForm (interactive form) of the document
            Form form = pdfDocument.Form;

            // Iterate over all form fields (WidgetAnnotation objects)
            foreach (WidgetAnnotation widget in form)
            {
                // Example: handle text box fields
                if (widget is TextBoxField textBox)
                {
                    // Retrieve the field name (FullName may be null, fallback to Name)
                    string fieldName = textBox.FullName ?? textBox.Name ?? string.Empty;

                    // Process a specific date field: convert from MM/dd/yyyy to yyyy-MM-dd
                    if (fieldName.Equals("DateField", StringComparison.OrdinalIgnoreCase))
                    {
                        if (textBox.Value != null)
                        {
                            string original = textBox.Value.ToString();
                            if (DateTime.TryParseExact(original, "MM/dd/yyyy", CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None, out DateTime dt))
                            {
                                // Reformat the date and assign back to the field
                                textBox.Value = dt.ToString("yyyy-MM-dd");
                            }
                        }
                    }
                    // Example: handle a numeric field that should be formatted with two decimals
                    else if (fieldName.Equals("AmountField", StringComparison.OrdinalIgnoreCase))
                    {
                        if (textBox.Value != null && double.TryParse(textBox.Value.ToString(),
                                                                     NumberStyles.Any,
                                                                     CultureInfo.InvariantCulture,
                                                                     out double amount))
                        {
                            textBox.Value = amount.ToString("F2", CultureInfo.InvariantCulture);
                        }
                    }
                    // Additional generic processing can be added here
                }
            }

            // Save the modified PDF using the prescribed save rule
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}