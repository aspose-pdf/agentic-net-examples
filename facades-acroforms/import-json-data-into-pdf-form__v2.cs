using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string fdfPath = "data.fdf";
        const string outputPath = "filled_form.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath) || !File.Exists(fdfPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        try
        {
            // Use Aspose.Pdf.Facades.Form to work with PDF forms
            using (Form form = new Form(pdfPath))
            {
                // Import form data (e.g., FDF). Missing fields throw an exception with a FieldName property.
                try
                {
                    using (FileStream fdfStream = File.OpenRead(fdfPath))
                    {
                        form.ImportFdf(fdfStream);
                    }
                }
                catch (Exception ex)
                {
                    // Attempt to read the optional FieldName property via reflection.
                    var fieldProp = ex.GetType().GetProperty("FieldName");
                    var missingField = fieldProp?.GetValue(ex) as string;
                    Console.Error.WriteLine($"Missing form field: {missingField ?? "<unknown>"}");
                }

                // Save the updated PDF with imported data
                form.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
