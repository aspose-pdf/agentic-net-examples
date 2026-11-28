using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "contract_form_v2.pdf";
        const string outputPath = "cleaned_options.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Use FormEditor to modify the PDF
            using (FormEditor editor = new FormEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Remove the list item "Deprecated" from the ListBox named "Options"
                editor.DelListItem("Options", "Deprecated");

                // Save the cleaned PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
